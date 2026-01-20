using FoodDelivery.API.Features.DriverAssignments.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.DriverAssignments.Queries;

public record GetDriverAssignmentsQuery : IRequest<List<DriverAssignmentDto>>;

public class GetDriverAssignmentsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetDriverAssignmentsQuery, List<DriverAssignmentDto>>
{
	public async Task<List<DriverAssignmentDto>> Handle(GetDriverAssignmentsQuery request, CancellationToken cancellationToken)
	{
		return await db.DriverAssignments.AsNoTracking()
			.Select(da => new DriverAssignmentDto(da.AssignmentId, da.OrderId, da.DriverId, da.AssignmentDate, da.DeliveryStatus))
			.ToListAsync(cancellationToken);
	}
}
