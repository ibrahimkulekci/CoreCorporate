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
        //AppDbContext c = new AppDbContext();
        private readonly AppDbContext _c;
        public GenericRepository(AppDbContext c)
        {
            _c = c;
        }

        public void Delete(T p)
        {
            _c.Remove(p);
            _c.SaveChanges();
        }

        public T GetById(int id)
        {
            return _c.Set<T>().Find(id);
        }

        public T GetByUrl(string p)
        {
            return _c.Set<T>().Find(p);
        }

        public List<T> GetListAll()
        {
            return _c.Set<T>().ToList();
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            return _c.Set<T>().Where(filter).ToList();
        }

        public void Insert(T p)
        {
            _c.Add(p);
            _c.SaveChanges();
        }

        public void Update(T p)
        {
            _c.Update(p);
            _c.SaveChanges();
        }
    }
}
