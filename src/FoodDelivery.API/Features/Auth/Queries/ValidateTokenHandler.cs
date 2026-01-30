using FoodDelivery.API.Features.Auth.DTOs;
using FoodDelivery.API.Features.Auth.Services;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FoodDelivery.API.Features.Auth.Queries;

public record ValidateTokenQuery(string Token) : IRequest<ValidateTokenResponseDto>;

public class ValidateTokenHandler(FoodDeliveryDbContext db, IJwtService jwtService) : IRequestHandler<ValidateTokenQuery, ValidateTokenResponseDto>
{
	public async Task<ValidateTokenResponseDto> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
	{
		var principal = jwtService.ValidateToken(request.Token);

		if (principal is null)
		{
			return new ValidateTokenResponseDto(false, null);
		}

		var userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

		if (!int.TryParse(userIdClaim, out var userId))
		{
			return new ValidateTokenResponseDto(false, null);
		}

		var user = await db.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

		if (user is null)
		{
			return new ValidateTokenResponseDto(false, null);
		}

		var userInfo = new UserInfoDto(user.UserId, user.Username, user.Email, user.Role);

		return new ValidateTokenResponseDto(true, userInfo);
	}
}
