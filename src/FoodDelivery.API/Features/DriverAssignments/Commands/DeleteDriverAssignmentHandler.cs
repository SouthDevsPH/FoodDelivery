using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.DriverAssignments.Commands;

public record DeleteDriverAssignmentCommand(int AssignmentId) : IRequest<bool>;

public class DeleteDriverAssignmentHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteDriverAssignmentCommand, bool>
{
	public async Task<bool> Handle(DeleteDriverAssignmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.DriverAssignments.FindAsync([request.AssignmentId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.DriverAssignments.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
