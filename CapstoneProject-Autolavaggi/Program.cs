using CapstoneProject_Autolavaggi.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conn = builder.Configuration.GetConnectionString("SqlServer")!;
builder.Services
    .AddDbContext<DataContext>(opt => opt.UseSqlServer(conn));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.LoginPath = "/Account/Login";
           options.LogoutPath = "/Account/Logout";
           options.AccessDeniedPath = "/Account/Login";
       });

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("Autenticato", cfg => cfg.RequireAuthenticatedUser())
    .AddPolicy("Cliente", cfg => cfg.RequireRole("Cliente"))
    .AddPolicy("Gestore", cfg => cfg.RequireRole("Gestore"))
    .AddPolicy("Amministratore", cfg => cfg.RequireRole("Amministratore"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
