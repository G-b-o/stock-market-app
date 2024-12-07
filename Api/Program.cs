using DotNetEnv;
using Api.Data;
using Api.Interfaces;
using Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

Console.WriteLine(Path.GetFullPath("../.env"));

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var postgresHost = Environment.GetEnvironmentVariable("POSTGRES_HOST");
    var postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
    var postgresUsername = Environment.GetEnvironmentVariable("POSTGRES_USER");
    var postgresDbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
    var connectionString = $"Host={postgresHost};Password={postgresPassword};Persist Security Info=True;Username={postgresUsername};Database={postgresDbName}";
    
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
