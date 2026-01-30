using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Auth.Commands;

public record LogoutCommand(string RefreshToken) : IRequest<bool>;

public class LogoutHandler(FoodDeliveryDbContext db) : IRequestHandler<LogoutCommand, bool>
{
	public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		var refreshToken = await db.RefreshTokens
			.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

		if (refreshToken is null)
		{
			return false;
		}

		refreshToken.IsRevoked = true;
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
