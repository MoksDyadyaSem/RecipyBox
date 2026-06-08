using System;
using RecipeBox.Dto.request;
using RecipeBox.Dto.response;
using RecipeBox.Entities;
using RecipeBox.Services;

namespace RecipeBox.Api;

public static class OrdersEndpoints
{
    public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/orders").WithTags("Orders");

        group.MapGet("/", (IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(orders.GetAllOrders().Select(mapper.Map)));

        group.MapGet("/status/{status}", (string status, IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
        {
            if (!Enum.TryParse<OrderStatus>(status, true, out var parsed))
            {
                return Results.BadRequest(new ErrorResponse { Message = "непонятный статус" });
            }

            return Results.Ok(orders.GetOrdersByStatus(parsed).Select(mapper.Map));
        });

        group.MapGet("/{id:int}", (int id, IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
        {
            var order = orders.GetOrderById(id);
            return order is null
                ? Results.NotFound(new ErrorResponse { Message = "заказ не найден" })
                : Results.Ok(mapper.Map(order));
        });

        group.MapPost("/", (CreateOrderRequest req, IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
        {
            try
            {
                var items = req.Items.Select(i => (i.RecipeId, i.Quantity)).ToList();
                var created = orders.CreateOrder(req.CustomerId, items);
                return Results.Created($"/api/orders/{created.Id}", mapper.Map(created));
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        group.MapPost("/{id:int}/cook", (int id, IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
        {
            try
            {
                return Results.Ok(mapper.Map(orders.StartCooking(id)));
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        group.MapPost("/{id:int}/deliver", (int id, IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
        {
            try
            {
                return Results.Ok(mapper.Map(orders.MarkDelivered(id)));
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        group.MapPost("/{id:int}/cancel", (int id, IOrderService orders, RecipeBox.Dto.IMapper mapper) =>
        {
            try
            {
                return Results.Ok(mapper.Map(orders.CancelOrder(id)));
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        group.MapDelete("/{id:int}", (int id, IOrderService orders) =>
        {
            try
            {
                var deleted = orders.DeleteOrder(id);
                return deleted
                    ? Results.Ok()
                    : Results.NotFound(new ErrorResponse { Message = "заказ не найден" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        return api;
    }
}
