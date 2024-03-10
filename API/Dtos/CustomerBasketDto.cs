using System.ComponentModel.DataAnnotations;

namespace API;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public List<BasketItemDto> Items { get; set; }
}
