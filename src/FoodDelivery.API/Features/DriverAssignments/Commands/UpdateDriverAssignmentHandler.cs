using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.DriverAssignments.Commands;

public record UpdateDriverAssignmentCommand(int AssignmentId, string? DeliveryStatus) : IRequest<bool>;

public class UpdateDriverAssignmentHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateDriverAssignmentCommand, bool>
{
	public async Task<bool> Handle(UpdateDriverAssignmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.DriverAssignments.FindAsync([request.AssignmentId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		if (request.DeliveryStatus is not null)
		{
			entity.DeliveryStatus = request.DeliveryStatus;
		}

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
