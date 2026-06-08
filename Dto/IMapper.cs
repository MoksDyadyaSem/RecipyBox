using RecipeBox.Dto.response;
using RecipeBox.Entities;

namespace RecipeBox.Dto;

public interface IMapper
{
    CategoryResponse Map(Category category);
    RecipeResponse Map(Recipe recipe);
    CustomerResponse Map(Customer customer);
    OrderResponse Map(Order order);
    ExpenseResponse Map(ExpenseTransaction transaction);
}
