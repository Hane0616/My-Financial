
using Microsoft.AspNetCore.Mvc; // 提供 MVC Controller 功能
using Microsoft.EntityFrameworkCore; // 提供 EF Core 資料庫查詢功能
using MyFinancial.Data; // 引入資料庫管理類別
using MyFinancial.Models; // 引入收支模型與錯誤頁面模型
using System.Diagnostics; // 提供錯誤追蹤資訊

namespace MyFinancial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context; // 儲存資料庫操作物件

        public HomeController(ApplicationDbContext context) // 由系統注入資料庫服務
        {
            _context = context; // 保存資料庫服務供其他方法使用
        }

        public async Task<IActionResult> Index() // 顯示首頁財務總覽
        {
            var today = DateTime.Today; // 取得今天的日期
            var monthStart = new DateTime(today.Year, today.Month, 1); // 計算本月第一天
            var nextMonthStart = monthStart.AddMonths(1); // 計算下個月第一天

            var transactions = await _context.Transactions // 取得本月收支紀錄
                .Where(t => t.Date >= monthStart && t.Date < nextMonthStart) // 限定在本月範圍
                .ToListAsync(); // 非同步讀取資料

            ViewBag.MonthlyIncome = transactions // 從本月紀錄計算收入
                .Where(t => t.Type == "Income") // 篩選收入
                .Sum(t => t.Amount); // 加總收入金額

            ViewBag.MonthlyExpense = transactions // 從本月紀錄計算支出
                .Where(t => t.Type == "Expense") // 篩選支出
                .Sum(t => t.Amount); // 加總支出金額

            ViewBag.MonthlyBalance = (decimal)ViewBag.MonthlyIncome - (decimal)ViewBag.MonthlyExpense; // 計算本月結餘

            ViewBag.ExpenseByCategory = transactions // 使用已取得的本月收支紀錄
                .Where(t => t.Type == "Expense") // 只保留支出紀錄
                .GroupBy(t => t.Category) // 依照支出分類分組
                .Select(g => new // 將每個分類整理成統計資料
                {
                    Category = g.Key, // 取得分類名稱
                    Total = g.Sum(t => t.Amount) // 計算該分類的支出總額
                })
                .OrderByDescending(g => g.Total) // 依支出金額由高到低排列
                .ToList(); // 將統計結果轉成清單


            return View(); // 將統計結果交給首頁
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
