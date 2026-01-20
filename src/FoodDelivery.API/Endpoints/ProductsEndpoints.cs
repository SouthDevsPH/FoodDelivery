using FoodDelivery.API.Features.Products.Commands;
using FoodDelivery.API.Features.Products.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class ProductsEndpoints
{
	public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/products");

		group.MapGet("", async (IMediator mediator) =>
		{
			var result = await mediator.Send(new GetProductsQuery());
			return Results.Ok(result);
		});

		group.MapGet("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetProductByIdQuery(id));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapGet("/merchant/{merchantId}", async (int merchantId, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetProductsByMerchantQuery(merchantId));
			return Results.Ok(result);
		});

		group.MapPost("", async (CreateProductCommand command, IMediator mediator) =>
		{
			var id = await mediator.Send(command);
			return Results.Created($"/api/products/{id}", id);
		});

		group.MapPut("/{id}", async (int id, UpdateProductCommand command, IMediator mediator) =>
		{
			if (command.ProductId != id)
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
			var result = await mediator.Send(new DeleteProductCommand(id));

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		return app;
	}
}
