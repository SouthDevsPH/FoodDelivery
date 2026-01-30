namespace FoodDelivery.API.Features.Auth.DTOs;

public record RegisterRequestDto(
	string Username,
	string Password,
	string? Email,
	string Role
);
