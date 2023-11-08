using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Vegastar.DataAccess;
using Vegastar.DataAccess.Options;
using Vegastar.DataAccess.Repositories;
using Vegastar.Domain.Contracts;
using Vegastar.Domain.Services;
using Vegastar.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsersContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString"));
});

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(UsersContext).Assembly);

builder.Services.Configure<RepositoryOptions>(builder.Configuration.GetSection(nameof(RepositoryOptions)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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