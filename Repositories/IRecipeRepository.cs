using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public interface IRecipeRepository
{
    IReadOnlyList<Recipe> GetAll();
    Recipe? GetById(int id);
    void Add(Recipe recipe);
    void Delete(int id);
}
