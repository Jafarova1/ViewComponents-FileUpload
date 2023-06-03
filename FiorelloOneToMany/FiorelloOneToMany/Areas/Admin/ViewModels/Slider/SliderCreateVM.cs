using Microsoft.Build.Framework;

namespace FiorelloOneToMany.Areas.Admin.ViewModels.Slider
{
    [Required]
    public class SliderCreateVM
    {
        public IFormFile Image { get; set; }
    }
}
