using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityScaffoldingDemo4.Data;
using IdentityScaffoldingDemo4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var adminUserName = builder.Configuration.GetSection("AdminCredentials")["UserName"];
    var adminPassword = builder.Configuration.GetSection("AdminCredentials")["Password"];
    await SeedData.SeedAdminUserAsync(scope.ServiceProvider, adminUserName, adminPassword);
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    await SeedData.SeedTestCaseStudiesAsync(dbContext, scope.ServiceProvider);
}

using (var scope = app.Services.CreateScope())
{
    await SeedAdmin.CreateUser(scope.ServiceProvider);
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}

app.Run();
