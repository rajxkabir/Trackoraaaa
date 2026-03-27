using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TimeSheetManager_services.Data;
using TimeSheetManager_services.Services.Interfaces;
using TimeSheetManager_services.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ✅ Controllers + JSON config
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ CORS (React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});

// ✅ Custom Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();

// 🔐 JWT CONFIG
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"])
        )
    };
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddAuthorization();

// 🔥 BUILD APP
var app = builder.Build();

// ✅ Middleware

app.UseCors("AllowReactApp");
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == 404)
    {
        await response.WriteAsJsonAsync(new
        {
            statusCode = 404,
            message = "API Not Found",
            success = false
        });
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // 🔥 FIRST
app.UseAuthorization();   // 🔥 SECOND

app.MapControllers();

app.Run();