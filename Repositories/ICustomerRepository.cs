using RecipeBox.Entities;

namespace RecipeBox.Repositories;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(int id);
    void Add(Customer customer);
    void Delete(int id);
}
