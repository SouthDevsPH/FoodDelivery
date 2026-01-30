using FoodDelivery.API.Features.Auth.DTOs;
using FoodDelivery.API.Features.Auth.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<LoginResponseDto?>;

public class LoginHandler(FoodDeliveryDbContext db, IJwtService jwtService, IConfiguration configuration) : IRequestHandler<LoginCommand, LoginResponseDto?>
{
	public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		var user = await db.Users
			.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

		if (user is null)
		{
			return null;
		}

		// Verify password hash
		if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
		{
			return null;
		}

		// Generate tokens
		var accessToken = jwtService.GenerateAccessToken(user);
		var refreshToken = jwtService.GenerateRefreshToken();

		// Store refresh token in database
		var refreshTokenExpiration = DateTime.UtcNow.AddDays(int.Parse(configuration["Jwt:RefreshTokenExpirationDays"] ?? "7"));

		var refreshTokenEntity = new RefreshToken
		{
			UserId = user.UserId,
			Token = refreshToken,
			ExpiresAt = refreshTokenExpiration,
			CreatedAt = DateTime.UtcNow,
			IsRevoked = false
		};

		db.RefreshTokens.Add(refreshTokenEntity);
		await db.SaveChangesAsync(cancellationToken);

		var userInfo = new UserInfoDto(user.UserId, user.Username, user.Email, user.Role);

		return new LoginResponseDto(
			accessToken,
			refreshToken,
			DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AccessTokenExpirationMinutes"] ?? "60")),
			userInfo
		);
	}
}
