using FoodDelivery.API.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// register DbContext using connection string from configuration
builder.Services.AddDbContext<FoodDelivery.Domain.Data.FoodDeliveryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDeliveryDb")));

// register MediatR handlers in this assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// map endpoints
app.MapMerchantsEndpoints()
	.MapProductsEndpoints()
	.MapUsersEndpoints()
	.MapOrdersEndpoints()
	.MapOrderItemsEndpoints();

app.Run();
