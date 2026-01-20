namespace FoodDelivery.API.Features.Products.DTOs;

public record ProductDto(int ProductId, int MerchantId, string Name, string? Description, decimal Price, int? StockQuantity);
