using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);

// ���������� ����� ������������ � CORS ��� ���������� ���� ���������� (��� ����������)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ����������� ApplicationDbContext � DI-����������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ���������� CORS
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
