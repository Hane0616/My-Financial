using Microsoft.EntityFrameworkCore;
using MyFinancial.Models;


namespace MyFinancial.Data
{
    // ApplicationDbContext 是管理 MyFinance 資料庫的類別
    // 繼承 DbContext，代表它具備 EF Core 的資料庫操作能力
    public class ApplicationDbContext : DbContext
    {
        // 建構子：建立 ApplicationDbContext 物件時接收資料庫設定
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)

            // 將資料庫設定傳給父類別 DbContext
            : base(options)
        {
        }

        // 定義收支資料集合
        // EF Core 會依照 Transaction 類別建立對應的資料表
        // 之後可以透過 Transactions 新增、查詢、修改及刪除紀錄
        public DbSet<Transaction> Transactions { get; set; }
    }
}