using Microsoft.EntityFrameworkCore;
using RecipeBox.database;
using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeBoxDbContext _db;

    public RecipeRepository(RecipeBoxDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<Recipe> GetAll()
    {
        return _db.Recipes.AsNoTracking().Include(recipe => recipe.Category).ToList();
    }

    public Recipe? GetById(int id)
    {
        return _db.Recipes.Include(recipe => recipe.Category).FirstOrDefault(recipe => recipe.Id == id);
    }

    public void Add(Recipe recipe)
    {
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var recipe = _db.Recipes.Find(id);
        if (recipe != null)
        {
            _db.Recipes.Remove(recipe);
            _db.SaveChanges();
        }
    }
}
