using RecipeBox.Dto.response;
using RecipeBox.Entities;

namespace RecipeBox.Dto;

public class Mapper : IMapper
{
    public CategoryResponse Map(Category category)
    {
        return new CategoryResponse(category.Id, category.Name);
    }

    public RecipeResponse Map(Recipe recipe)
    {
        return new RecipeResponse(
            recipe.Id,
            recipe.CategoryId,
            recipe.Category?.Name ?? "",
            recipe.Title,
            recipe.CookTimeMinutes,
            recipe.Price);
    }

    public CustomerResponse Map(Customer customer)
    {
        return new CustomerResponse(customer.Id, customer.FullName, customer.Phone, customer.RegisteredAt);
    }

    public OrderResponse Map(Order order)
    {
        var items = order.Items.Select(i => new OrderItemResponse(
            i.RecipeId,
            i.Recipe?.Title ?? "",
            i.Quantity,
            i.UnitPrice,
            i.LineTotal)).ToArray();

        return new OrderResponse(
            order.Id,
            order.CustomerId,
            order.Customer?.FullName ?? "",
            order.CreatedAt,
            order.Status.ToString(),
            order.TotalAmount,
            items);
    }

    public ExpenseResponse Map(ExpenseTransaction transaction)
    {
        return new ExpenseResponse(transaction.Id, transaction.Description, transaction.Amount, transaction.OccurredOn);
    }
}
