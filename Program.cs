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



// Configure the HTTP request pipeline.
var connectionString = builder.Configuration.GetConnectionString("ApDbConnectionString");
builder.Services.AddDbContext<ApplicationDBcontext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=PostManagement}");

app.Run();
