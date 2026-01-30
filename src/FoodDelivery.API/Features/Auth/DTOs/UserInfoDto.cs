namespace FoodDelivery.API.Features.Auth.DTOs;

public record UserInfoDto(int UserId, string Username, string? Email, string Role);
