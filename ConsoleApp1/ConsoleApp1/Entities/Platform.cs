using System.ComponentModel.DataAnnotations;

namespace GameStore.Entities;
public class Platform
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Game> Games { get; set; } = new();
}
