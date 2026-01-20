using FoodDelivery.API.Features.DriverAssignments.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.API.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.DriverAssignments.Queries;

public record GetDriverAssignmentByIdQuery(int AssignmentId) : IRequest<DriverAssignmentDto?>;

public class GetDriverAssignmentByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetDriverAssignmentByIdQuery, DriverAssignmentDto?>
{
	public async Task<DriverAssignmentDto?> Handle(GetDriverAssignmentByIdQuery request, CancellationToken cancellationToken)
	{
		var da = await db.DriverAssignments.AsNoTracking().FirstOrDefaultAsync(da => da.AssignmentId == request.AssignmentId, cancellationToken);

		if (da is null)
		{
			return null;
		}

		return new DriverAssignmentDto(da.AssignmentId, da.OrderId, da.DriverId, da.AssignmentDate, (DeliveryStatusEnum)da.DeliveryStatusId);
	}
}

