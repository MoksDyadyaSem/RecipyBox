using RecipeBox.Entities;

namespace RecipeBox.Services;

public interface IOrderService
{
    IReadOnlyList<Order> GetAllOrders();
    IReadOnlyList<Order> GetOrdersByStatus(OrderStatus status);
    Order? GetOrderById(int id);
    Order CreateOrder(int customerId, IReadOnlyList<(int RecipeId, int Quantity)> items);
    Order StartCooking(int orderId);
    Order MarkDelivered(int orderId);
    Order CancelOrder(int orderId);
    bool DeleteOrder(int id);
}
