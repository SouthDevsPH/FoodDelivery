using FoodDelivery.API.Features.DriverAssignments.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.API.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.DriverAssignments.Queries;

public record GetDriverAssignmentsByDriverQuery(int DriverId) : IRequest<List<DriverAssignmentDto>>;

public class GetDriverAssignmentsByDriverHandler(FoodDeliveryDbContext db) : IRequestHandler<GetDriverAssignmentsByDriverQuery, List<DriverAssignmentDto>>
{
	public async Task<List<DriverAssignmentDto>> Handle(GetDriverAssignmentsByDriverQuery request, CancellationToken cancellationToken)
	{
		return await db.DriverAssignments.AsNoTracking()
			.Where(da => da.DriverId == request.DriverId)
			.Select(da => new DriverAssignmentDto(da.AssignmentId, da.OrderId, da.DriverId, da.AssignmentDate, (DeliveryStatusEnum)da.DeliveryStatusId))
			.ToListAsync(cancellationToken);
	}
}

