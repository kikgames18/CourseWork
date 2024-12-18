using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ����������� ����������� ������
app.UseStaticFiles();

// ��������� �������� �� ���������
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/html/login.html");
    });
});

app.Run();