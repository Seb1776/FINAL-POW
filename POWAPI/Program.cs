using Microsoft.EntityFrameworkCore;
using POWAPI.Models;
using POWAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSettings>();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseMongoDB(mongoSettings?.ConnectionURI ?? "", mongoSettings?.DatabaseName ?? ""));

builder.Services.AddScoped<IStudentService, StudentService>();

// Add services to the container.
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