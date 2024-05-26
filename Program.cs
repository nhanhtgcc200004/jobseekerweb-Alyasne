using EnterpriceWeb.Mailutils;
using finalyearproject.Models;
using finalyearproject.SubSystem.Mailutils;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<ApplicationDBcontext>();



// Configure the HTTP request pipeline.
var connectionString = builder.Configuration.GetConnectionString("ApDbConnectionString");
builder.Services.AddDbContext<ApplicationDBcontext>(options =>  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("/Error/NotFound");
    }
});
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}");

app.Run();
