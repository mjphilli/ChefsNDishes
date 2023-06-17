#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [Range(1, 5)]
    public int Tastiness { get; set; }
    [Required(ErrorMessage = "Must have at least 0 calories")]
    [Range(0, int.MaxValue)]
    public int Calories { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Required]
    public int ChefId { get; set; }
    public Chef? Creator { get; set; }
}