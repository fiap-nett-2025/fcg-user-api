using FCG.User.API;
using FCG.User.API.Configurations;
using FCG.User.Application;
using FCG.User.Application.Middleware;
using FCG.User.Application.Services;
using FCG.User.Application.Services.Interfaces;
using FCG.User.Domain.Entities;
using FCG.User.Domain.Interfaces;
using FCG.User.Infra.Data;
using FCG.User.Infra.Data.Context;
using FCG.User.Infra.Data.Messaging.Config;
using FCG.User.Infra.Data.Repository;
using FCG.User.Infra.Data.Seedings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ Connection String
builder.Services.AddDbContextFactory<FCGDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)
    );
});

// ✅ Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<FCGDbContext>()
.AddDefaultTokenProviders();

// ✅ JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var chaveSecreta = jwtSettings["Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta!)),
        ClockSkew = TimeSpan.Zero
    };
});

#region RabbitMq ( Not Used)
/*var rabbitSection = builder.Configuration.GetSection("RabbitMq");

var rabbitSettingsSection = rabbitSection.GetSection("Settings");
if (!rabbitSettingsSection.Exists())
    throw new InvalidOperationException("Section 'RabbitMqSettings' not found in configuration.");
builder.Services.Configure<RabbitMqOptions>(rabbitSettingsSection);

var queuesSectionRabbit = rabbitSection.GetSection("Queues");
if (!queuesSectionRabbit.Exists())
    throw new InvalidOperationException("Section 'Queues' not found in configuration.");
builder.Services.Configure<QueuesOptions>(queuesSectionRabbit);

builder.Services.ConfigureRabbitMq();*/
#endregion

#region Amazon SQS
var messagingSection = builder.Configuration.GetSection("Messaging");
if (!messagingSection.Exists())
    throw new InvalidOperationException("Section 'Messaging' not found in configuration.");

var queuesSection = messagingSection.GetSection("Queues");
builder.Services.Configure<QueuesOptions>(queuesSection);

builder.Services.ConfigureAmazonSQS(builder.Configuration);
#endregion

// ✅ Serviços
builder.Services.AddTransient<JwtService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserGameLibraryRepository, UserGameLibraryRepository>();
builder.Services.AddTransient<IUserGameLibraryServices, UserGameLibraryService>();
builder.Services.AddSwaggerConfiguration();
builder.Services.ConfigureServices();

// ✅ Worker
builder.Services.AddHostedService<Worker>();

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();

// ✅ Controllers
builder.Services.AddControllers();

builder.Logging.AddJsonConsole();



var app = builder.Build();

// ✅ Pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwaggerConfiguration();
    try
    {
        await using var scope = app.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<FCGDbContext>();
        if (dbContext.Database.EnsureCreated())
        {
            await RoleAndAdminSeeding.SeedAsync(scope.ServiceProvider);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao criar banco de dados: {ex.Message}");
    }
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/Error");
app.UseHsts();



app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();