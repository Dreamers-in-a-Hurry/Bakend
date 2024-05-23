using Fitshirt.Api;
using Fitshirt.Domain;
using Fitshirt.Infrastructure;
using Fitshirt.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddApiServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<FitshirtDbContext>())
{
    context!.Database.EnsureCreated();
}

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