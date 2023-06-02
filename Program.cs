using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{ 
    x.LoginPath = "/User/Login";
    x.AccessDeniedPath = "/User/AccessDenied";
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

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

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "login",
    pattern: "{controller=User}/{action=Login}");

app.MapControllerRoute(
    name: "all-news",
    pattern: "Xeberler",
    defaults: new { controller = "News", action = "Index" });

app.MapControllerRoute(
    name: "add-news",
    pattern: "Elave",
    defaults: new { controller = "News", action = "AddNews" });

app.MapControllerRoute(
    name: "news-details",
    pattern: "Xeber/{id:int}",
    defaults: new { controller = "News", action = "Details" });

app.MapDefaultControllerRoute();

app.Run();
