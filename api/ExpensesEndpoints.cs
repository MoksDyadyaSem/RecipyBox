using System.Linq;
using RecipeBox.Dto.request;
using RecipeBox.Dto.response;
using RecipeBox.Services;

namespace RecipeBox.Api;

public static class ExpensesEndpoints
{
    public static RouteGroupBuilder MapExpensesEndpoints(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/expenses").WithTags("Expenses");

        group.MapGet("/", (IExpenseService svc, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(svc.GetAllTransactions().Select(mapper.Map)));

        group.MapGet("/recent/{count:int}", (int count, IExpenseService svc, RecipeBox.Dto.IMapper mapper) =>
            Results.Ok(svc.GetRecentTransactions(count).Select(mapper.Map)));

        group.MapGet("/{id:int}", (int id, IExpenseService svc, RecipeBox.Dto.IMapper mapper) =>
        {
            var transaction = svc.GetTransactionById(id);
            return transaction is null
                ? Results.NotFound(new ErrorResponse { Message = "Транзакция не найдена" })
                : Results.Ok(mapper.Map(transaction));
        });

        group.MapPost("/", (CreateExpenseRequest req, IExpenseService svc, RecipeBox.Dto.IMapper mapper) =>
        {
            if (string.IsNullOrWhiteSpace(req.Description))
            {
                return Results.BadRequest(new ErrorResponse { Message = "Описание обязательно" });
            }

            if (string.IsNullOrWhiteSpace(req.Date))
            {
                return Results.BadRequest(new ErrorResponse { Message = "Нужно ввести дату" });
            }

            var created = svc.AddTransaction(req.Description, req.Amount, req.Date);
            return Results.Created($"/api/expenses/{created.Id}", mapper.Map(created));
        });

        group.MapDelete("/{id:int}", (int id, IExpenseService svc) =>
        {
            var deleted = svc.DeleteTransaction(id);
            return deleted
                ? Results.Ok()
                : Results.NotFound(new ErrorResponse { Message = "Транзакция не найдена" });
        });

        return api;
    }
}
