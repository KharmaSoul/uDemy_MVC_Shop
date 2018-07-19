using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepository<Item> where Item : BaseEntity
    {
        IQueryable<Item> Collection();
        void Commit();
        void Delete(string sID);
        Item Find(string sID);
        void Insert(Item item);
        void Update(Item item);
    }
}