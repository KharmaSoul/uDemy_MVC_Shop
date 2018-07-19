using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ModelProductCategory
    {
        public string ID { get; set; }
        public string Category { get; set; }

        #region SUB - Constructor
        public ModelProductCategory()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        #endregion
    }
}
