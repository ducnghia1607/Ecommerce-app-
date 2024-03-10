using System.ComponentModel.DataAnnotations;

namespace API;

public class BasketItemDto
{
    [Required]
    public int Id { get; set; }
    [Required]

    public string ProductName { get; set; }
    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "The price must be greater than 1 ")]
    public decimal Price { get; set; }
    [Required]
    public string PictureUrl { get; set; }
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "The quantity must be at least 1 ")]
    public int Quantity { get; set; }

    [Required]
    public string Brand { get; set; }

    [Required]
    public string Type { get; set; }
}
