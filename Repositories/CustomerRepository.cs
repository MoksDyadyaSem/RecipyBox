using Microsoft.EntityFrameworkCore;
using RecipeBox.database;
using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly RecipeBoxDbContext _db;

    public CustomerRepository(RecipeBoxDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<Customer> GetAll()
    {
        return _db.Customers.AsNoTracking().ToList();
    }

    public Customer? GetById(int id)
    {
        return _db.Customers.FirstOrDefault(customer => customer.Id == id);
    }

    public void Add(Customer customer)
    {
        _db.Customers.Add(customer);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var customer = _db.Customers.Find(id);
        if (customer != null)
        {
            _db.Customers.Remove(customer);
            _db.SaveChanges();
        }
    }
}
