using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public interface IOrderRepository
{
    IReadOnlyList<Order> GetAll();
    Order? GetById(int id);
    void Add(Order order);
    void Update(Order order);
    void Delete(int id);
}
