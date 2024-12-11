using Microsoft.Extensions.Configuration;
using WebApplication2.Data;
using WebApplication2.Services;

public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddScoped<SurveyService>();
    services.AddScoped<QuestionService>();
    services.AddScoped<AnswerService>();
    services.AddScoped<ResultService>();
    services.AddScoped<CategoryService>();

    services.AddControllers();
    // Добавьте другие необходимые сервисы, такие как Swagger, если нужно
}