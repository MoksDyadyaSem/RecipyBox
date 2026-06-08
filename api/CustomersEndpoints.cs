using System;
using RecipeBox.Dto.request;
using RecipeBox.Dto.response;
using RecipeBox.Services;

namespace RecipeBox.Api;

public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomersEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/customers").WithTags("Customers");

        group.MapGet("/", (ICustomerService customers, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(customers.GetAll().Select(mapper.Map)));

        group.MapGet("/{id:int}", (int id, ICustomerService customers, RecipeBox.Dto.IMapper mapper) =>
        {
            var customer = customers.GetById(id);
            return customer is null
                ? Results.NotFound(new ErrorResponse { Message = "клиент не найден" })
                : Results.Ok(mapper.Map(customer));
        });

        group.MapPost("/", (CreateCustomerRequest req, ICustomerService customers, RecipeBox.Dto.IMapper mapper) =>
        {
            try
            {
                var created = customers.Register(req.FullName, req.Phone);
                return Results.Created($"/api/customers/{created.Id}", mapper.Map(created));
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                return Results.BadRequest(new ErrorResponse { Message = ex.Message });
            }
        });

        group.MapDelete("/{id:int}", (int id, ICustomerService customers) =>
        {
            var deleted = customers.Delete(id);
            return deleted
                ? Results.Ok()
                : Results.NotFound(new ErrorResponse { Message = "клиент не найден" });
        });

        return api;
    }
}
