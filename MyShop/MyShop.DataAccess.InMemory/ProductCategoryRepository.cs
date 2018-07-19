using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache ocCache = MemoryCache.Default;
        List<ModelProductCategory> ltCategories;

        #region SUB - Constructor
        public ProductCategoryRepository()
        {
            ltCategories = ocCache["ProductsCategories"] as List<ModelProductCategory>;

            if (ltCategories == null)
            {
                ltCategories = new List<ModelProductCategory>();
            }
        }
        #endregion

        #region SUB - Commit
        public void Commit()
        {
            ocCache["ProductsCategories"] = ltCategories;
        }
        #endregion
        #region SUB - Insert
        public void Insert(ModelProductCategory cItem)
        {
            ltCategories.Add(cItem);
        }
        #endregion
        #region SUB - Update
        public void Update(ModelProductCategory cItem)
        {
            ModelProductCategory cItemToUpdate = ltCategories.Find(p => p.ID == cItem.ID);

            if (cItemToUpdate != null)
            {
                cItemToUpdate = cItem;
            }
            else
            {
                throw new Exception("Product category not found!");
            }
        }
        #endregion
        #region SUB - Find
        public ModelProductCategory Find(string sID)
        {
            ModelProductCategory cItemToFind = ltCategories.Find(p => p.ID == sID);

            if (cItemToFind != null)
            {
                return cItemToFind;
            }
            else
            {
                throw new Exception("Product category not found!");
            }
        }
        #endregion
        #region SUB - Collection
        public IQueryable<ModelProductCategory> Collection()
        {
            return ltCategories.AsQueryable();
        }
        #endregion
        #region SUB - Delete
        public void Delete(string sID)
        {
            ModelProductCategory cItemToDelete = ltCategories.Find(p => p.ID == sID);

            if (cItemToDelete != null)
            {
                ltCategories.Remove(cItemToDelete);
            }
            else
            {
                throw new Exception("Product category not found!");
            }
        }
        #endregion
    }
}
