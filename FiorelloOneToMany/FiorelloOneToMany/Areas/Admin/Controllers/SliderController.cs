
using FiorelloOneToMany.Areas.Admin.ViewModels.Slider;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Elearn.Models;
using FiorelloOneToMany.Helpers;

namespace Elearn.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<SliderVM> list = new();
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            foreach (Slider slider in sliders)
            {
                SliderVM model = new()
                {
                    Id = slider.Id,
                    Logo = slider.Logo,
                    Title = slider.Title,
                    Description = slider.Description,
                    //CreatedDate = slider.CreatedDate.ToString("MMMM dd, yyyy"),
                    Status = slider.Status,
                };

                list.Add(model);
            }
            return View(list);
        }


        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();

            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();

            SliderDetailVM model = new()
            {
                Logo = dbSlider.Logo,
                Title = dbSlider.Title,
                //CreatedDate = dbSlider.CreatedDate.ToString("MMMM dd, yyyy"),
                Status = dbSlider.Status,
                Description = dbSlider.Description,
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image","Plaese select only image file");
                return View();
            }

            if (request.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;

            await request.Image.SaveFileAsync( fileName, _env.WebRootPath, "img");

        //await FileExtensions.SaveFileAsync(request.Image,fileName, _env.WebRootPath, "img");

            //string fileName=Guid.NewGuid().ToString()+"_"+request.Image.FileName;

            //string path = Path.Combine(_env.WebRootPath, "img",fileName);
    
            //using(FileStream stream = new(path, FileMode.Create))
            //{
            //    await request.Image.CopyToAsync(stream);
            //}

            Slider slider = new()
            {
                Image = fileName
            };



            await _context.Sliders.AddAsync(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
