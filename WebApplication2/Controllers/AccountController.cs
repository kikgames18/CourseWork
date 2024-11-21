using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

public class AccountController : Controller
{
    private readonly string connectionString;

    public AccountController(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(connectionString));

    }

    [HttpPost]
    [Route("Account/Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var query = "SELECT * FROM account WHERE login = @login AND password = @password";
            var account = await connection.QueryFirstOrDefaultAsync(query, new { login = loginModel.Username, password = loginModel.Password });

            if (account != null)
            {
                // Здесь можно добавить логику для установки авторизационного cookie
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

public class LoginModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}
