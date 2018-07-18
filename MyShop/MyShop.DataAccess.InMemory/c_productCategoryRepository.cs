using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class c_productCategoryRepository
    {
        ObjectCache ocCache = MemoryCache.Default;
        List<c_modelProductCategory> ltCategories;

        #region SUB - Constructor
        public c_productCategoryRepository()
        {
            ltCategories = ocCache["ProductsCategories"] as List<c_modelProductCategory>;

            if (ltCategories == null)
            {
                ltCategories = new List<c_modelProductCategory>();
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
        public void Insert(c_modelProductCategory cItem)
        {
            ltCategories.Add(cItem);
        }
        #endregion
        #region SUB - Update
        public void Update(c_modelProductCategory cItem)
        {
            c_modelProductCategory cItemToUpdate = ltCategories.Find(p => p.ID == cItem.ID);

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
        public c_modelProductCategory Find(string sID)
        {
            c_modelProductCategory cItemToFind = ltCategories.Find(p => p.ID == sID);

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
        public IQueryable<c_modelProductCategory> Collection()
        {
            return ltCategories.AsQueryable();
        }
        #endregion
        #region SUB - Delete
        public void Delete(string sID)
        {
            c_modelProductCategory cItemToDelete = ltCategories.Find(p => p.ID == sID);

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
