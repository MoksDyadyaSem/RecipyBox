using Microsoft.EntityFrameworkCore;
using RecipeBox.database;
using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly RecipeBoxDbContext _db;

    public OrderRepository(RecipeBoxDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<Order> GetAll()
    {
        return _db.Orders
            .AsNoTracking()
            .Include(order => order.Customer)
            .Include(order => order.Items)
            .ThenInclude(item => item.Recipe)
            .ToList();
    }

    public Order? GetById(int id)
    {
        return _db.Orders
            .Include(order => order.Customer)
            .Include(order => order.Items)
            .ThenInclude(item => item.Recipe)
            .FirstOrDefault(order => order.Id == id);
    }

    public void Add(Order order)
    {
        _db.Orders.Add(order);
        _db.SaveChanges();
    }

    public void Update(Order order)
    {
        _db.Orders.Update(order);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var order = _db.Orders.Find(id);
        if (order != null)
        {
            _db.Orders.Remove(order);
            _db.SaveChanges();
        }
    }
}
