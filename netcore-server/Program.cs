using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using netcore_server.Database;
using netcore_server.Repositories;
using netcore_server.Services;
using System.Text.Json;
using netcore_server.Middlewares;



var builder = WebApplication.CreateBuilder(args);

// export port
builder.WebHost.UseUrls("http://localhost:5210");

// Controllers
builder.Services
    .AddControllers()
    .AddJsonOptions(o => {
        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        o.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFE", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // port vite
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();


//build app
var app = builder.Build();

app.UsePathBase("/api/v1");


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowFE");
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();