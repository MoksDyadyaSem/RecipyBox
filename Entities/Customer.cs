namespace RecipeBox.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }

    public Customer() { }

    public Customer(string fullName, string phone, DateTime registeredAt)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Имя не может быть пустым", nameof(fullName));
        }

        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Телефон не может быть пустым", nameof(phone));
        }

        FullName = fullName;
        Phone = phone;
        RegisteredAt = registeredAt;
    }
}
