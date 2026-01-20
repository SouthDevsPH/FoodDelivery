namespace FoodDelivery.API.Features.Users.DTOs;

public record UserDto(int UserId, string Username, string? Email, string Role);
