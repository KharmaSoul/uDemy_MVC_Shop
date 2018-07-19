using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ModelProduct : BaseEntity
    {
        [StringLength(20)]
        [DisplayName("Produc Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [Range(0, 1000)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Image")]
        public string Image { get; set; }
    }
}
