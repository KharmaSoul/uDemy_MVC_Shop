using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class c_modelProductCategory
    {
        public string ID { get; set; }
        public string Category { get; set; }

        #region SUB - Constructor
        public c_modelProductCategory()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        #endregion
    }
}
