using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;
public class Order
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}
