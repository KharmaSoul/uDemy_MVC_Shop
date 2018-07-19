using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.ViewModels
{
    public class ViewModelProductManager
    {
        public ModelProduct Product { get; set; }
        public IEnumerable<ModelProductCategory> ProductCategories { get; set; }
    }
}
