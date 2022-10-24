using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.Models
{
    public class Product : BaseEntity
    {
        public string  Title { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Decimal Price { get; set; }
        public string Desc { get; set; }
        public DateTime CreateDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
