using FoodDelivery.API.Features.Users.Commands;
using FoodDelivery.API.Features.Users.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class UsersEndpoints
{
	public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/users");

		group.MapGet("", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUsersQuery());
            return Results.Ok(result);
        });

        group.MapGet("/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));

            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });

        group.MapPost("", async (CreateUserCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/users/{id}", id);
        });

        group.MapPut("/{id}", async (int id, UpdateUserCommand command, IMediator mediator) =>
        {
            if (command.UserId != id)
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
            var result = await mediator.Send(new DeleteUserCommand(id));

            if (!result)
            {
                return Results.NotFound();
            }

			return Results.NoContent();
		});

		return app;
	}
}
