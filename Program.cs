using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConsultaExterna.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConsultaExternaContext");

builder.Services.AddDbContext<ConsultaExternaContext>(options =>
    options.UseNpgsql(connectionString));
// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
