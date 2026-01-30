namespace FoodDelivery.API.Features.Auth.DTOs;

public record ValidateTokenResponseDto(bool IsValid, UserInfoDto? User);
