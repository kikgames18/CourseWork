using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Подключение статических файлов
app.UseStaticFiles();

// Настройка маршрута по умолчанию
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/html/login.html");
    });
});

app.Run();