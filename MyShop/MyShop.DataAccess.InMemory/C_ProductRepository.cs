using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class C_ProductRepository
    {
        ObjectCache ocCache = MemoryCache.Default;
        List<C_ModelProduct> ltProducts;

        #region SUB - Constructor
        public C_ProductRepository()
        {
            ltProducts = ocCache["Products"] as List<C_ModelProduct>;

            if (ltProducts == null)
            {
                ltProducts = new List<C_ModelProduct>();
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
        public void Insert(C_ModelProduct cProduct)
        {
            ltProducts.Add(cProduct);
        }
        #endregion
        #region SUB - Update
        public void Update(C_ModelProduct cProduct)
        {
            C_ModelProduct cProductToUpdate = ltProducts.Find(p => p.ID == cProduct.ID);

            if (cProductToUpdate != null)
            {
                cProductToUpdate = cProduct;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        #endregion
        #region SUB - Find
        public C_ModelProduct Find(string sID)
        {
            C_ModelProduct cProductToFind = ltProducts.Find(p => p.ID == sID);

            if (cProductToFind != null)
            {
                return cProductToFind;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        #endregion
        #region SUB - Collection
        public IQueryable<C_ModelProduct> Collection()
        {
            return ltProducts.AsQueryable();
        }
        #endregion
        #region SUB - Delete
        public void Delete(string sID)
        {
            C_ModelProduct cProductToDelete = ltProducts.Find(p => p.ID == sID);

            if (cProductToDelete != null)
            {
                ltProducts.Remove(cProductToDelete);
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        #endregion
    }
}
