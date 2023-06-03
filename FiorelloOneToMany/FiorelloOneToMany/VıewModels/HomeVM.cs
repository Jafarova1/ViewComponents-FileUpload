﻿using Elearn.Models;
using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.VıewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider>? Sliders { get; set; }
        public IEnumerable<Expert> Experts { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
