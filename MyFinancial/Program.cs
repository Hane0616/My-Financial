using Microsoft.EntityFrameworkCore;          // 引入 EF Core，讓我們能設定資料庫
using MyFinancial.Data;                       // 引入剛建立的 ApplicationDbContext

var builder = WebApplication.CreateBuilder(args); // 建立網站應用程式的設定與服務容器

builder.Services.AddControllersWithViews();   // 註冊 MVC 服務，讓網站支援 Controller 與 View

builder.Services.AddDbContext<ApplicationDbContext>(options => // 註冊資料庫服務，讓其他程式可以使用它
    options.UseSqlite("Data Source=MyFinance.db"));             // 指定使用 SQLite，資料庫檔案名稱為 MyFinance.db

var app = builder.Build();                    // 建立網站應用程式

if (!app.Environment.IsDevelopment())         // 如果目前不是開發環境
{
    app.UseExceptionHandler("/Home/Error");   // 發生未處理的錯誤時，導向錯誤頁面
    app.UseHsts();                            // 啟用 HSTS，要求瀏覽器使用 HTTPS
}

app.UseHttpsRedirection();                    // 將 HTTP 請求重新導向 HTTPS
app.UseRouting();                             // 啟用網址路由功能

app.UseAuthorization();                       // 啟用授權中介軟體

app.MapStaticAssets();                        // 提供 CSS、JavaScript、圖片等靜態資源
app.MapControllerRoute(                       // 設定 MVC 網址規則
    name: "default",                          // 這條路由的名稱
    pattern: "{controller=Home}/{action=Index}/{id?}") // 預設進入 Home 控制器的 Index 頁面

    .WithStaticAssets();                      // 啟用這個專案範本的靜態資源處理功能

app.Run();                                    // 啟動網站，開始接收請求