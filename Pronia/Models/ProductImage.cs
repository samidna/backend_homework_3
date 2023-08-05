using System.ComponentModel.DataAnnotations;

namespace Pronia.Models;

public class ProductImage : BaseEntity
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product? product { get; set; }

}
