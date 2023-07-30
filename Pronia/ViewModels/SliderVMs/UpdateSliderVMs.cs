using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels.SliderVMs
{
    public record UpdateSliderVMs
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required, MaxLength(60)]
        public string Title { get; set; }
        [Required, MaxLength(40)]
        public string ButtonText { get; set; }
        [Required, MaxLength(40)]
        public string Offer { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
 