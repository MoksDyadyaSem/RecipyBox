using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public interface ICategoryRepository
{
    IReadOnlyList<Category> GetAll();
    Category? GetById(int id);
    void Add(Category category);
    void Delete(int id);
}
