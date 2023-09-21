using Space.WebApp.Service.Concretes;
using Space.WebApp.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var expireTimeSpan = DateTime.Now.AddMinutes(30).Subtract(DateTime.Now);

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(q => 
{
    q.Cookie.HttpOnly = true;
    q.SlidingExpiration = true;
    q.Cookie.Name = "AuthCookie";
    q.LoginPath = "/Auth/Index/";
    q.LogoutPath = "/Auth/Logout/";
    q.ExpireTimeSpan = expireTimeSpan;
    q.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddSession();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPriceHistoryService, PriceHistoryService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();