using RecipeBox.Entities;
using RecipeBox.Repositories;

namespace RecipeBox.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRecipeRepository _recipeRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IRecipeRepository recipeRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _recipeRepository = recipeRepository;
    }

    public IReadOnlyList<Order> GetAllOrders()
    {
        return _orderRepository.GetAll()
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
    }

    public IReadOnlyList<Order> GetOrdersByStatus(OrderStatus status)
    {
        return _orderRepository.GetAll()
            .Where(o => o.Status == status)
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
    }

    public Order? GetOrderById(int id)
    {
        return _orderRepository.GetById(id);
    }

    public Order CreateOrder(int customerId, IReadOnlyList<(int RecipeId, int Quantity)> items)
    {
        if (_customerRepository.GetById(customerId) == null)
        {
            throw new InvalidOperationException("Клиент не найден");
        }

        if (items.Count == 0)
        {
            throw new ArgumentException("Нужно хотя бы одно блюдо");
        }

        Order order = new Order(customerId, DateTime.UtcNow);

        foreach (var item in items)
        {
            Recipe? recipe = _recipeRepository.GetById(item.RecipeId);
            if (recipe == null)
            {
                throw new InvalidOperationException($"Рецепт {item.RecipeId} не найден");
            }

            order.AddItem(new OrderItem(item.RecipeId, item.Quantity, recipe.Price));
        }

        _orderRepository.Add(order);
        return order;
    }

    public Order StartCooking(int orderId)
    {
        Order? order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            throw new InvalidOperationException("Заказ не найден");
        }

        order.StartCooking();
        _orderRepository.Update(order);
        return order;
    }

    public Order MarkDelivered(int orderId)
    {
        Order? order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            throw new InvalidOperationException("Заказ не найден");
        }

        order.MarkDelivered();
        _orderRepository.Update(order);
        return order;
    }

    public Order CancelOrder(int orderId)
    {
        Order? order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            throw new InvalidOperationException("Заказ не найден");
        }

        order.Cancel();
        _orderRepository.Update(order);
        return order;
    }

    public bool DeleteOrder(int id)
    {
        Order? order = _orderRepository.GetById(id);
        if (order == null)
        {
            return false;
        }

        if (order.Status == OrderStatus.Cooking)
        {
            throw new InvalidOperationException("Уже готовится — не удаляем");
        }

        _orderRepository.Delete(id);
        return true;
    }
}
