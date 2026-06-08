using RecipeBox.Dto;
using RecipeBox.Entities;
using RecipeBox.Repositories;

namespace RecipeBox.Services;

public class CatalogService : ICatalogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRecipeRepository _recipeRepository;

    public CatalogService(ICategoryRepository categoryRepository, IRecipeRepository recipeRepository)
    {
        _categoryRepository = categoryRepository;
        _recipeRepository = recipeRepository;
    }

    public IReadOnlyList<Category> GetCategories()
    {
        return _categoryRepository.GetAll().OrderBy(c => c.Name).ToList();
    }

    public Category? GetCategoryById(int id)
    {
        return _categoryRepository.GetById(id);
    }

    public Category AddCategory(string name)
    {
        Category category = new Category(name);
        _categoryRepository.Add(category);
        return category;
    }

    public bool DeleteCategory(int id)
    {
        var existing = _categoryRepository.GetById(id);
        if (existing is null)
        {
            return false;
        }

        _categoryRepository.Delete(id);
        return true;
    }

    public IReadOnlyList<Recipe> GetAllRecipes()
    {
        return _recipeRepository.GetAll().OrderBy(r => r.Title).ToList();
    }

    public IReadOnlyList<Recipe> SearchRecipes(string searchTerm)
    {
        string term = searchTerm;
        return _recipeRepository.GetAll()
            .Where(r => r.Title.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(r => r.Title)
            .ToList();
    }

    public IReadOnlyList<Recipe> GetQuickRecipes(int maxCookTimeMinutes)
    {
        return _recipeRepository.GetAll()
            .Where(r => r.CookTimeMinutes <= maxCookTimeMinutes)
            .OrderBy(r => r.CookTimeMinutes)
            .ToList();
    }

    public IEnumerable<CategoryStatResult> GetCategoryStatistics()
    {
        List<CategoryStatResult> result = new List<CategoryStatResult>();
        IReadOnlyList<Recipe> recipes = _recipeRepository.GetAll();

        foreach (Category category in _categoryRepository.GetAll())
        {
            List<Recipe> inCategory = recipes.Where(r => r.CategoryId == category.Id).ToList();

            CategoryStatResult item = new CategoryStatResult();
            item.CategoryName = category.Name;
            item.RecipeCount = inCategory.Count;
            item.AvgCookMinutes = inCategory.Count == 0 ? 0 : (int)inCategory.Average(r => r.CookTimeMinutes);
            item.TotalPrice = inCategory.Sum(r => r.Price);
            result.Add(item);
        }

        return result.OrderByDescending(x => x.RecipeCount).ToList();
    }

    public Recipe AddRecipe(int categoryId, string title, int cookTimeMinutes, decimal price)
    {
        if (_categoryRepository.GetById(categoryId) == null)
        {
            throw new InvalidOperationException("Нет такой категории");
        }

        Recipe recipe = new Recipe(categoryId, title, cookTimeMinutes, price);
        _recipeRepository.Add(recipe);
        return recipe;
    }
}
