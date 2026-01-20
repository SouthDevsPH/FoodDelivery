namespace FoodDelivery.API.Features.Orders.DTOs;

public record OrderDto(int OrderId, int UserId, int MerchantId, DateTime? OrderDate, string? Status, decimal TotalAmount, string Address, DateTime? DeliveryTime);
