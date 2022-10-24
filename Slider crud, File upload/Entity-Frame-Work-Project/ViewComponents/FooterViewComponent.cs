using Entity_Frame_Work_Project.Data;
using Entity_Frame_Work_Project.Models;
using Entity_Frame_Work_Project.Services;
using Entity_Frame_Work_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;
        private readonly AppDbContext _context;

        public FooterViewComponent(LayoutService layoutService, AppDbContext context)
        {
            _layoutService = layoutService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settingDatas = await _layoutService.GetDatasFromSetting();

            string email = settingDatas["Email"];

            IEnumerable<Social> socials = await _context.Socials.ToListAsync();

            FooterVM footerVM = new FooterVM
            {
                Email = email,
                Socials = socials
            };

            return await Task.FromResult(View(footerVM));        
        }
    }
}
