using FoodDelivery.API.Features.Orders.Commands;
using FoodDelivery.API.Features.Orders.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class OrdersEndpoints
{
	public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/orders")
			.RequireAuthorization();

		group.MapGet("", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrdersQuery());
            return Results.Ok(result);
        });

        group.MapGet("/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrderByIdQuery(id));

            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });

        group.MapGet("/user/{userId}", async (int userId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrdersByUserQuery(userId));
            return Results.Ok(result);
        });

        group.MapPost("", async (CreateOrderCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/orders/{id}", id);
        });

        group.MapPut("/{id}", async (int id, UpdateOrderCommand command, IMediator mediator) =>
        {
            if (command.OrderId != id)
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
            var result = await mediator.Send(new DeleteOrderCommand(id));

            if (!result)
            {
                return Results.NotFound();
            }

			return Results.NoContent();
		});

		return app;
	}
}
