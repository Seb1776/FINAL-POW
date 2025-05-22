using POWAPI.Models;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, Role>().AddMongoDbStores<User, Role, Guid>
(
    "mongodb+srv://sebastianwho1776:lakezurich123@powcluster.vgn3vfq.mongodb.net/?retryWrites=true&w=majority&appName=POWCluster", "pow"
);

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