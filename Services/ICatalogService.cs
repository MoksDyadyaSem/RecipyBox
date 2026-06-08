using RecipeBox.Dto;
using RecipeBox.Entities;

namespace RecipeBox.Services;

public interface ICatalogService
{
    IReadOnlyList<Category> GetCategories();
    Category? GetCategoryById(int id);
    Category AddCategory(string name);
    bool DeleteCategory(int id);
    IReadOnlyList<Recipe> GetAllRecipes();
    IReadOnlyList<Recipe> SearchRecipes(string searchTerm);
    IReadOnlyList<Recipe> GetQuickRecipes(int maxCookTimeMinutes);
    IEnumerable<CategoryStatResult> GetCategoryStatistics();
    Recipe AddRecipe(int categoryId, string title, int cookTimeMinutes, decimal price);
}
