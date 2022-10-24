using Entity_Frame_Work_Project.Data;
using Entity_Frame_Work_Project.Models;
using Entity_Frame_Work_Project.Services;
using Entity_Frame_Work_Project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;

        public HomeController(AppDbContext context, LayoutService layoutService)
        {
            _context = context;
            _layoutService = layoutService;
        }
        public async Task <IActionResult> Index()
        {
            //HttpContext.Session.SetString("name", "Cavidan");
            //Response.Cookies.Append("surname", "Qedirli", new CookieOptions { MaxAge = TimeSpan.FromSeconds(25)});

            Dictionary<string, string> settingDatas = await _layoutService.GetDatasFromSetting();
            int take = int.Parse(settingDatas["HomeTakeProduct"]);

            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail sliderDetail = await _context.SliderDetails.FirstOrDefaultAsync();
            IEnumerable<Category> categories = await _context.Categories.Where(m => m.IsDeleted == false).ToListAsync();
            IEnumerable<Product> products = await _context.Products
                .Where(m => m.IsDeleted == false)
                .Include(m => m.Category)
                .Include(m => m.ProductImages).Take(take).ToListAsync();

            HomeVM model = new HomeVM
            {
                Sliders = sliders,
                SliderDetail = sliderDetail,
                Categories = categories,
                Products = products

            };

            return View(model);
        }

        [HttpPost]
        //ajaxla yazanda bunu yazmirsan
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            //var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            var dbProduct = await GetProductById(id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> basket = GetBasket();

            UpdateBasket(basket, dbProduct.Id);

            //var exitsProduct = basket.Find(m=> m.Id == dbProduct.Id);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return Ok();
        
        }

        private void UpdateBasket(List<BasketVM> basket,int id)
        {
            BasketVM exitsProduct = basket.FirstOrDefault(m => m.Id == id);


            if (exitsProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = id,
                    Count = 1
                });
            }
            else
            {
                exitsProduct.Count++;
            }
        }

        private async Task<Product> GetProductById(int? id)
        {
            return await _context.Products.FindAsync(id);

        }

        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }











        //public IActionResult Test()
        //{
        //    var sessionData = HttpContext.Session.GetString("name");
        //    var cookieData = Request.Cookies["surname"];
        //    return Json(sessionData +"-"+ cookieData);
        
        //}
       
    }
}
