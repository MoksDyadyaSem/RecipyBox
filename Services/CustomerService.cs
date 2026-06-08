using RecipeBox.Entities;
using RecipeBox.Repositories;

namespace RecipeBox.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IReadOnlyList<Customer> GetAll()
    {
        return _customerRepository.GetAll().OrderBy(c => c.FullName).ToList();
    }

    public Customer? GetById(int id)
    {
        return _customerRepository.GetById(id);
    }

    public Customer Register(string fullName, string phone)
    {
        Customer customer = new Customer(fullName, phone, DateTime.UtcNow);
        _customerRepository.Add(customer);
        return customer;
    }

    public bool Delete(int id)
    {
        var existing = _customerRepository.GetById(id);
        if (existing is null)
        {
            return false;
        }

        _customerRepository.Delete(id);
        return true;
    }
}
