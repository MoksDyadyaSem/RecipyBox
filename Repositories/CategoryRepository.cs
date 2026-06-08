using Microsoft.EntityFrameworkCore;
using RecipeBox.database;
using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly RecipeBoxDbContext _db;

    public CategoryRepository(RecipeBoxDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<Category> GetAll()
    {
        return _db.Categories.AsNoTracking().ToList();
    }

    public Category? GetById(int id)
    {
        return _db.Categories.FirstOrDefault(category => category.Id == id);
    }

    public void Add(Category category)
    {
        _db.Categories.Add(category);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var category = _db.Categories.Find(id);
        if (category != null)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
    }
}
