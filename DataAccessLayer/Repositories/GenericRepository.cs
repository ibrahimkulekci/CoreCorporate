using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        Context c = new Context();

        public void Delete(T p)
        {
            c.Remove(p);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            return c.Set<T>().Find(id);
        }

        public T GetByUrl(string p)
        {
            return c.Set<T>().Find(p);
        }

        public List<T> GetListAll()
        {
            return c.Set<T>().ToList();
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            return c.Set<T>().Where(filter).ToList();
        }

        public void Insert(T p)
        {
            c.Add(p);
            c.SaveChanges();
        }

        public void Update(T p)
        {
            c.Update(p);
            c.SaveChanges();
        }
    }
}
