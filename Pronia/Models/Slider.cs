using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required,MaxLength(200)]
        public string Description { get; set; }
        [Required,MaxLength(60)]
        public string Title { get; set; }
        [Required,MaxLength(40)]
        public string ButtonText { get; set; }
        [Required,MaxLength(40)]
        public string Offer { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
