using RecipeBox.Entities;

namespace RecipeBox.Services;

public interface ICustomerService
{
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(int id);
    Customer Register(string fullName, string phone);
    bool Delete(int id);
}
