using FoodDelivery.API.Features.Merchants.Commands;
using FoodDelivery.API.Features.Merchants.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class MerchantsEndpoints
{
    public static void MapMerchantsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/merchants");

        group.MapGet("", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetMerchantsQuery());
            return Results.Ok(result);
        });

        group.MapGet("/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetMerchantByIdQuery(id));

            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });

        group.MapPost("", async (AddOrEditMerchantCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/merchants/{id}", id);
        });

        group.MapPut("/{id}", async (int id, AddOrEditMerchantCommand command, IMediator mediator) =>
        {
            if (command.MerchantId.HasValue && command.MerchantId.Value != id)
            {
                return Results.BadRequest();
            }

            var updated = command with { MerchantId = id };
            var result = await mediator.Send(updated);

            if (result == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });
    }
}
