using System.ComponentModel.DataAnnotations;
using GameStore.Entities;

public class Game
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }
    
    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }

    public int? PlatformId { get; set; }
    public Platform? Platform { get; set; }

    public decimal Price { get; set; }
}