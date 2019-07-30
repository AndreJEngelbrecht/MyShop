using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using System.Data.Entity;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<SR> : IRepository<SR> where SR : BaseEntity
    {
        internal DataContext context;
        internal DbSet<SR> dBset;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dBset = context.Set<SR>();
        }
        public IQueryable<SR> Collection()
        {
            return dBset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var iM = Find(Id);
            if (context.Entry(iM).State == EntityState.Detached)
                dBset.Attach(iM);

            dBset.Remove(iM);
        }

        public SR Find(string Id)
        {
            return dBset.Find(Id);
        }

        public void Insert(SR iM)
        {
            dBset.Add(iM);
        }

        public void Update(SR im)
        {
            dBset.Attach(im);
            context.Entry(im).State = EntityState.Modified;
        }
    }

}
