using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts

{
    public interface IRepository<IM> where IM : BaseEntity
    {
        IQueryable<IM> Collection();
        void Commit();
        void Delete(string Id);
        IM Find(string Id);
        void Insert(IM iM);
        void Update(IM im);
    }
}