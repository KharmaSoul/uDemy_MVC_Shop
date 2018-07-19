using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache ocCache = MemoryCache.Default;
        List<ModelProduct> ltProducts;

        #region SUB - Constructor
        public ProductRepository()
        {
            ltProducts = ocCache["Products"] as List<ModelProduct>;

            if (ltProducts == null)
            {
                ltProducts = new List<ModelProduct>();
            }
        }
        #endregion

        #region SUB - Commit
        public void Commit()
        {
            ocCache["Products"] = ltProducts;
        }
        #endregion
        #region SUB - Insert
        public void Insert(ModelProduct cItem)
        {
            ltProducts.Add(cItem);
        }
        #endregion
        #region SUB - Update
        public void Update(ModelProduct cItem)
        {
            ModelProduct cItemToUpdate = ltProducts.Find(p => p.ID == cItem.ID);

            if (cItemToUpdate != null)
            {
                cItemToUpdate = cItem;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        #endregion
        #region SUB - Find
        public ModelProduct Find(string sID)
        {
            ModelProduct cItemToFind = ltProducts.Find(p => p.ID == sID);

            if (cItemToFind != null)
            {
                return cItemToFind;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        #endregion
        #region SUB - Collection
        public IQueryable<ModelProduct> Collection()
        {
            return ltProducts.AsQueryable();
        }
        #endregion
        #region SUB - Delete
        public void Delete(string sID)
        {
            ModelProduct cItemToDelete = ltProducts.Find(p => p.ID == sID);

            if (cItemToDelete != null)
            {
                ltProducts.Remove(cItemToDelete);
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        #endregion
    }
}
