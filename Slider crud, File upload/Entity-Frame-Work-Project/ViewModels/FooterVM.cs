using Entity_Frame_Work_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.ViewModels
{
    public class FooterVM
    {
        public string Email { get; set; }
        public IEnumerable<Social> Socials { get; set; }
    }
}
