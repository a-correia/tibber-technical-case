using System.Text.Json.Serialization;
using CleaningService.Repositories;
using CleaningService.Services;
using Microsoft.OpenApi.Models;
using Domain = CleaningService.Domain.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cleaning API", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<ICleanService, CleanService>();
builder.Services.AddScoped<ICleanRepository, CleanRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging();

var port = builder.Configuration["Server:Port"];
builder.WebHost.ConfigureKestrel(options => { options.ListenAnyIP(int.Parse(port)); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();