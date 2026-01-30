namespace FoodDelivery.API.Features.Auth.DTOs;

public record LoginResponseDto(
	string AccessToken,
	string RefreshToken,
	DateTime ExpiresAt,
	UserInfoDto User
);
