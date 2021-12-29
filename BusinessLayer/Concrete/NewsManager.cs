using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsManager:INewsService
    {
        INewsDal _newsDal;

        public NewsManager(INewsDal newsDal)
        {
            _newsDal = newsDal;
        }

        public List<News> GetList()
        {
            return _newsDal.GetListAll().OrderByDescending(x => x.NewsID).ToList();
        }

        public void TAdd(News p)
        {
            _newsDal.Insert(p);
        }

        public void TDelete(News p)
        {
            _newsDal.Delete(p);
        }

        public News TGetById(int id)
        {
            return _newsDal.GetById(id);
        }

        public News TGetByUrl(string p)
        {
            return _newsDal.GetByUrl(p);
        }

        public void TUpdate(News p)
        {
            _newsDal.Update(p);
        }
    }
}
