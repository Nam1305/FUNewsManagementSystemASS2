using FUNewsManagementSystem.BusinessLogic.Service;
using FUNewsManagementSystem.DataAccess.Contexts;
using FUNewsManagementSystem.DataAccess.Repositories;
using FUNewsManagementSystem.WebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
// ✅ Thêm dòng này: Đăng ký Session vào DI container
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // session tồn tại 30 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSignalR(); // ✅ thêm dòng này


//Đăng ký DbContext vào DI container
builder.Services.AddDbContext<FunewsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

//// DI cho Repository + Service
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<NewsArticleService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, SessionUserIdProvider>(); // ✅ thêm dòng này

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// phải gọi trước MapRazorPages
app.UseSession();

app.MapRazorPages();
app.MapHub<NotificationHub>("/notificationHub"); // ✅ Map Hub

app.Run();
