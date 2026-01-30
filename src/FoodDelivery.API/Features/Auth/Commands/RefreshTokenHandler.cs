using FoodDelivery.API.Features.Auth.DTOs;
using FoodDelivery.API.Features.Auth.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Auth.Commands;

public record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResponseDto?>;

public class RefreshTokenHandler(FoodDeliveryDbContext db, IJwtService jwtService, IConfiguration configuration) : IRequestHandler<RefreshTokenCommand, LoginResponseDto?>
{
	public async Task<LoginResponseDto?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var refreshToken = await db.RefreshTokens
			.Include(rt => rt.User)
			.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

		if (refreshToken is null || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow)
		{
			return null;
		}

		// Revoke old refresh token
		refreshToken.IsRevoked = true;

		// Generate new tokens
		var accessToken = jwtService.GenerateAccessToken(refreshToken.User);
		var newRefreshToken = jwtService.GenerateRefreshToken();

		// Store new refresh token
		var refreshTokenExpiration = DateTime.UtcNow.AddDays(int.Parse(configuration["Jwt:RefreshTokenExpirationDays"] ?? "7"));

		var newRefreshTokenEntity = new RefreshToken
		{
			UserId = refreshToken.UserId,
			Token = newRefreshToken,
			ExpiresAt = refreshTokenExpiration,
			CreatedAt = DateTime.UtcNow,
			IsRevoked = false
		};

		db.RefreshTokens.Add(newRefreshTokenEntity);
		await db.SaveChangesAsync(cancellationToken);

		var userInfo = new UserInfoDto(
			refreshToken.User.UserId,
			refreshToken.User.Username,
			refreshToken.User.Email,
			refreshToken.User.Role
		);

		return new LoginResponseDto(
			accessToken,
			newRefreshToken,
			DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AccessTokenExpirationMinutes"] ?? "60")),
			userInfo
		);
	}
}
