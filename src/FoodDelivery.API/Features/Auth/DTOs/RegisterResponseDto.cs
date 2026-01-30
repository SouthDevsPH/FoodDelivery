namespace FoodDelivery.API.Features.Auth.DTOs;

public record RegisterResponseDto(
	int UserId,
	string Username,
	string? Email,
	string Role,
	string Message
);
