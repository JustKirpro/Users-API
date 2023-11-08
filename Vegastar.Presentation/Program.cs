using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Vegastar.DataAccess;
using Vegastar.DataAccess.Options;
using Vegastar.DataAccess.Repositories;
using Vegastar.Domain.Contracts;
using Vegastar.Domain.Services;
using Vegastar.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsersContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString"));
});

builder.Services
    .AddControllers(o =>
    {
        o.Filters.Add<ExceptionFilter>();
    })
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(UsersContext).Assembly);

builder.Services.Configure<RepositoryOptions>(builder.Configuration.GetSection(nameof(RepositoryOptions)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Users API",
        Description = "ASP.NET 6 Web API for Vegastar test task"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();