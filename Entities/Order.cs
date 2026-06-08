namespace RecipeBox.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }

    public Customer? Customer { get; set; }
    public List<OrderItem> Items { get; set; } = new();

    public Order() { }

    public Order(int customerId, DateTime createdAt)
    {
        CustomerId = customerId;
        CreatedAt = createdAt;
        Status = OrderStatus.New;
    }

    public void AddItem(OrderItem item)
    {
        item.Order = this;
        Items.Add(item);
        TotalAmount = Items.Sum(i => i.LineTotal);
    }

    public void StartCooking()
    {
        if (Status != OrderStatus.New)
        {
            throw new InvalidOperationException("Заказ уже не новый");
        }

        if (Items.Count == 0)
        {
            throw new InvalidOperationException("Пустой заказ");
        }

        Status = OrderStatus.Cooking;
    }

    public void MarkDelivered()
    {
        if (Status != OrderStatus.Cooking)
        {
            throw new InvalidOperationException("Сначала надо начать готовить");
        }

        Status = OrderStatus.Delivered;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
        {
            throw new InvalidOperationException("Уже отдали — поздно отменять");
        }

        Status = OrderStatus.Cancelled;
    }
}
