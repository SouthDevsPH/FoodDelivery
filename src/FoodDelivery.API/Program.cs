using FoodDelivery.API.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FoodDelivery API",
        Version = "v1",
        Description = "A comprehensive food delivery system API with CQRS pattern"
    });

    options.TagActionsBy(api =>
    {
        var route = api.RelativePath ?? "";
        if (route.StartsWith("api/merchants")) return ["Merchants"];
        if (route.StartsWith("api/products")) return ["Products"];
        if (route.StartsWith("api/users")) return ["Users"];
        if (route.StartsWith("api/orders")) return ["Orders"];
        if (route.StartsWith("api/orderitems")) return ["Order Items"];
        if (route.StartsWith("api/payments")) return ["Payments"];
        if (route.StartsWith("api/driverassignments")) return ["Driver Assignments"];
        if (route.StartsWith("api/driverwallets")) return ["Driver Wallets"];
        if (route.StartsWith("api/wallettransactions")) return ["Wallet Transactions"];
        return ["Other"];
    });

    // Ensure enum values display as strings
    options.UseInlineDefinitionsForEnums();
});

// register DbContext using connection string from configuration
builder.Services.AddDbContext<FoodDelivery.Domain.Data.FoodDeliveryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDeliveryDb")));

// register MediatR handlers in this assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodDelivery API v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "FoodDelivery API Documentation";
    options.DisplayRequestDuration();
    options.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();

// map endpoints
app.MapMerchantsEndpoints()
    .MapProductsEndpoints()
    .MapUsersEndpoints()
    .MapOrdersEndpoints()
    .MapOrderItemsEndpoints()
    .MapPaymentsEndpoints()
    .MapDriverAssignmentsEndpoints()
    .MapDriverWalletsEndpoints()
    .MapWalletTransactionsEndpoints();

app.Run();
