using Microsoft.AspNetCore.Mvc;                 // 提供 MVC Controller 與 HTTP 回應功能
using Microsoft.EntityFrameworkCore;             // 提供 EF Core 資料庫查詢功能
using MyFinancial.Data;                          // 引入資料庫管理類別
using MyFinancial.Models;                        // 引入 Transaction 收支模型

namespace MyFinancial.Controllers
{
    public class TransactionsController : Controller // 管理收支頁面的 Controller
    {
        private readonly ApplicationDbContext _context; // 儲存資料庫操作物件

        public TransactionsController(ApplicationDbContext context) // 建構子：由系統注入資料庫服務
        {
            _context = context;                    // 將資料庫服務保存起來，供其他方法使用
        }

        public async Task<IActionResult> Index()   // 顯示所有收支紀錄
        {
            var transactions = await _context.Transactions // 從資料庫取得收支資料
                .OrderByDescending(t => t.Date)             // 日期由新到舊排列
                .ToListAsync();                             // 非同步讀取並轉成清單

            return View(transactions);             // 將資料交給 Index.cshtml 顯示
        }

        public IActionResult Create()              // 顯示新增收支表單
        {
            return View(new Transaction());                         // 回傳空白表單頁面
        }

        [HttpPost]                                 // 只處理透過 POST 送出的表單
        [ValidateAntiForgeryToken]                 // 防止跨站請求偽造（CSRF）
        public async Task<IActionResult> Create(Transaction transaction) // 接收使用者輸入的收支資料
        {
            if (!ModelState.IsValid)               // 檢查資料是否通過模型驗證
            {
                return View(transaction);          // 驗證失敗時重新顯示表單與原本輸入
            }

            _context.Transactions.Add(transaction); // 將新收支加入待儲存的資料集合
            await _context.SaveChangesAsync();      // 將資料真正寫入 SQLite

            return RedirectToAction(nameof(Index)); // 儲存完成後返回收支列表
        }

        // 顯示編輯收支的表單
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // 確認網址有沒有傳入紀錄編號
            {
                return NotFound(); // 沒有編號就回傳 404
            }

            var transaction = await _context.Transactions.FindAsync(id); // 依照編號尋找收支紀錄

            if (transaction == null) // 確認資料庫是否找到這筆紀錄
            {
                return NotFound(); // 找不到紀錄就回傳 404
            }

            return View(transaction); // 把原本的資料傳給編輯頁面
        }

        // 接收編輯表單並儲存修改
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id) // 確認網址編號與表單資料編號一致
            {
                return NotFound();
            }

            if (!ModelState.IsValid) // 檢查輸入資料是否符合驗證規則
            {
                return View(transaction); // 驗證失敗就重新顯示表單
            }

            _context.Update(transaction); // 標記這筆收支資料已修改
            await _context.SaveChangesAsync(); // 將修改儲存至 SQLite

            return RedirectToAction(nameof(Index)); // 儲存完成後返回收支列表
        }


        // 顯示刪除確認頁面
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // 確認網址是否有傳入紀錄編號
            {
                return NotFound(); // 沒有編號就回傳 404
            }

            var transaction = await _context.Transactions // 從資料庫尋找指定紀錄
                .FirstOrDefaultAsync(t => t.Id == id); // 找出 Id 符合的資料

            if (transaction == null) // 確認紀錄是否存在
            {
                return NotFound(); // 找不到資料就回傳 404
            }

            return View(transaction); // 將資料交給刪除確認頁面
        }

        // 接收刪除確認並執行刪除
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id); // 依照編號尋找紀錄

            if (transaction != null) // 確認紀錄存在
            {
                _context.Transactions.Remove(transaction); // 將紀錄標記為刪除
                await _context.SaveChangesAsync(); // 將刪除操作寫入 SQLite
            }

            return RedirectToAction(nameof(Index)); // 刪除完成後返回收支列表
        }

    }
}