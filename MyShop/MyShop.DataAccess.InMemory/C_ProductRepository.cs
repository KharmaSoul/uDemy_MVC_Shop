﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class c_productRepository
    {
        ObjectCache ocCache = MemoryCache.Default;
        List<c_modelProduct> ltProducts;

        #region SUB - Constructor
        public c_productRepository()
        {
            ltProducts = ocCache["Products"] as List<c_modelProduct>;

            if (ltProducts == null)
            {
                ltProducts = new List<c_modelProduct>();
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
        public void Insert(c_modelProduct cItem)
        {
            ltProducts.Add(cItem);
        }
        #endregion
        #region SUB - Update
        public void Update(c_modelProduct cItem)
        {
            c_modelProduct cItemToUpdate = ltProducts.Find(p => p.ID == cItem.ID);

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
        public c_modelProduct Find(string sID)
        {
            c_modelProduct cItemToFind = ltProducts.Find(p => p.ID == sID);

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
        public IQueryable<c_modelProduct> Collection()
        {
            return ltProducts.AsQueryable();
        }
        #endregion
        #region SUB - Delete
        public void Delete(string sID)
        {
            c_modelProduct cItemToDelete = ltProducts.Find(p => p.ID == sID);

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
