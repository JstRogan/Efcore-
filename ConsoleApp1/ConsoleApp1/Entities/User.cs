using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;
public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public List<Order> Orders { get; set; } = new();
}
