namespace FoodDelivery.API.Features.Feed.DTOs;

public record PopularNowDto(
	int MerchantId,
	string Name,
	string? Email,
	string? PhoneNumber,
	string? AddressTitle,
	double Latitude,
	double Longitude,
	int OrderCount
);
