using Entity_Frame_Work_Project.Data;
using Entity_Frame_Work_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BlogViewComponent(AppDbContext context)
        {
            _context = context;        
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await GetAllBlogs()));
        }

        private async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }
    }
}
