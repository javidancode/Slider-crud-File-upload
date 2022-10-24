using Entity_Frame_Work_Project.Data;
using Entity_Frame_Work_Project.Helpers;
using Entity_Frame_Work_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.IsDeleted).ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            //if (!slider.Photo.ContentType.Contains("image/"))
            //{
            //    ModelState.AddModelError("Photo", "Please choose correct image type");
            //    return View();
            //}

            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Please choose correct image type");
                return View();
            }

            //if ((slider.Photo.Length / 1024) > 400)
            //{
            //     ModelState.AddModelError("Photo", "Please choose correct image size");
            //     return View();
            //}

            if (!slider.Photo.CheckFileSize(400))
            {
                ModelState.AddModelError("Photo", "Please choose correct image size");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

            //Path.Combine(_env.WebRootPath, "img", fileName);
            string path = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(stream);
            }

            slider.Image = fileName;

            await _context.Sliders.AddAsync(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await GetById(id);

            if (slider is null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "img", slider.Image);

            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}

            Helper.SliderImgDelete(path);

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<Slider> GetById(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }
    }
}
