using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<Item> : IRepository<Item> where Item : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;

        List<Item> ltItems;
        string sClassName;

        #region SUB - Constructor
        public InMemoryRepository()
        {
            sClassName = typeof(Item).Name;
            ltItems = cache[sClassName] as List<Item>;

            if (ltItems == null)
            {
                ltItems = new List<Item>();
            }
        }
        #endregion
        #region SUB - Commit
        public void Commit()
        {
            cache[sClassName] = ltItems;
        }
        #endregion
        #region SUB - Collection
        public IQueryable<Item> Collection()
        {
            return ltItems.AsQueryable();
        }
        #endregion
        #region SUB - Delete
        public void Delete(string sID)
        {
            Item itemToDelete = ltItems.Find(p => p.ID == sID);

            if (itemToDelete != null)
            {
                ltItems.Remove(itemToDelete);

            }
            else
            {
                throw new Exception(sClassName + " not found!");
            }
        }
        #endregion
        #region SUB - Find
        public Item Find(string sID)
        {
            Item itemToUpdate = ltItems.Find(p => p.ID == sID);

            if (itemToUpdate != null)
            {
                return itemToUpdate;
            }
            else
            {
                throw new Exception(sClassName + " not found!");
            }
        }
        #endregion
        #region SUB - Insert
        public void Insert(Item item)
        {
            ltItems.Add(item);
        }
        #endregion
        #region SUB - Update
        public void Update(Item item)
        {
            Item itemToUpdate = ltItems.Find(p => p.ID == item.ID);

            if (itemToUpdate != null)
            {
                itemToUpdate = item;
            }
            else
            {
                throw new Exception(sClassName + " not found!");
            }
        }
        #endregion
    }
}
