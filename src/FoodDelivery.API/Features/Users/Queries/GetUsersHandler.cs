using FoodDelivery.API.Features.Users.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Users.Queries;

public record GetUsersQuery : IRequest<List<UserDto>>;

public class GetUsersHandler(FoodDeliveryDbContext db) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
	public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
	{
		return await db.Users.AsNoTracking()
			.Where(u => u.IsActive)
			.Select(u => new UserDto(u.UserId, u.Username, u.Email, u.Role))
			.ToListAsync(cancellationToken);
	}
}
