using FoodDelivery.API.Endpoints;
using FoodDelivery.API.Features.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "FoodDelivery API",
        Version = "v1",
        Description = "A comprehensive food delivery system API with CQRS pattern - JWT Authentication Enabled"
    });

    options.TagActionsBy(api =>
    {
        var route = api.RelativePath ?? "";
        if (route.StartsWith("api/auth")) return ["Authentication"];
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

// register JWT service
builder.Services.AddSingleton<IJwtService, JwtService>();

// configure JWT authentication
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

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
app.MapAuthEndpoints()
    .MapMerchantsEndpoints()
    .MapProductsEndpoints()
    .MapUsersEndpoints()
    .MapOrdersEndpoints()
    .MapOrderItemsEndpoints()
    .MapPaymentsEndpoints()
    .MapDriverAssignmentsEndpoints()
    .MapDriverWalletsEndpoints()
    .MapWalletTransactionsEndpoints();

app.Run();
