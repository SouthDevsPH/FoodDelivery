using FoodDelivery.API.Features.OrderItems.Commands;
using FoodDelivery.API.Features.OrderItems.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class OrderItemsEndpoints
{
	public static IEndpointRouteBuilder MapOrderItemsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/orderitems");

		group.MapGet("", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrderItemsQuery());
            return Results.Ok(result);
        });

        group.MapGet("/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrderItemByIdQuery(id));

            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });

        group.MapGet("/order/{orderId}", async (int orderId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrderItemsByOrderQuery(orderId));
            return Results.Ok(result);
        });

        group.MapPost("", async (CreateOrderItemCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/orderitems/{id}", id);
        });

        group.MapPut("/{id}", async (int id, UpdateOrderItemCommand command, IMediator mediator) =>
        {
            if (command.OrderItemId != id)
            {
                return Results.BadRequest();
            }

            var result = await mediator.Send(command);

            if (!result)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteOrderItemCommand(id));

            if (!result)
            {
                return Results.NotFound();
            }

			return Results.NoContent();
		});

		return app;
	}
}
