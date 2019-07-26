using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<IM> where IM : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<IM> Items;
        string ClassName;

        public InMemoryRepository()
        {
            ClassName = typeof(IM).Name;
            Items = cache[ClassName] as List<IM>;
            if(Items == null)
            {
                Items = new List<IM>();
            }
        }

        public void Commit()
        {
            cache[ClassName] = Items;
        }

        public void Insert(IM iM)
        {
            Items.Add(iM);
        }

        public void Update(IM im)
        {
            IM imToUpdate = Items.Find(i => i.Id == im.Id);
            if(imToUpdate != null)
            {
                imToUpdate = im;
            }
            else
            {
                throw new Exception(ClassName + " Not found");
            }
        }

        public IM Find(string Id)
        {
            IM im = Items.Find(i => i.Id == Id);

            if(im != null)
            {
                return im;
            }
            else
            {
                throw new Exception(ClassName + " Not found");
            }
        }

        public IQueryable<IM> Collection()
        {
            return Items.AsQueryable();
        }

        public void Delete(string Id)
        {
            IM imToDelete = Items.Find(i => i.Id == Id);

            if(imToDelete != null)
            {
                Items.Remove(imToDelete);
            }
            else
            {
                throw new Exception(ClassName + " Not found");
            }
        }

    }

    
}
