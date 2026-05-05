using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConsultaExterna.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ConsultaExternaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConsultaExternaContext") ?? throw new InvalidOperationException("Connection string 'ConsultaExternaContext' not found.")));
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");
// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
