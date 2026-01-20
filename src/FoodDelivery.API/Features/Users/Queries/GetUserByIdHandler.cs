using FoodDelivery.API.Features.Users.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Users.Queries;

public record GetUserByIdQuery(int UserId) : IRequest<UserDto?>;

public class GetUserByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetUserByIdQuery, UserDto?>
{
	public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var u = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

		if (u is null)
		{
			return null;
		}

		return new UserDto(u.UserId, u.Username, u.Email, u.Role);
	}
}
