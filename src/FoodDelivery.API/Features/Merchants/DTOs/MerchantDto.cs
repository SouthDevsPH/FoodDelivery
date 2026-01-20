namespace FoodDelivery.API.Features.Merchants.DTOs;

public record MerchantDto(int MerchantId, int UserId, string Name, string? Address, string? PhoneNumber, string? Email);
