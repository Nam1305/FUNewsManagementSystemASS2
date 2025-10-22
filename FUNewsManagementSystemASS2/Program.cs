using FUNewsManagementSystem.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// ⚙️ Đăng ký DbContext vào DI container
builder.Services.AddDbContext<FunewsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
