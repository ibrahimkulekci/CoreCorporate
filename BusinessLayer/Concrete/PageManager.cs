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
    public class PageManager:IPageService
    {
        IPageDal _pageDal;

        public PageManager(IPageDal pageDal)
        {
            _pageDal = pageDal;
        }

        public List<Page> GetList()
        {
            return _pageDal.GetListAll().OrderByDescending(x=>x.PageID).ToList();
        }

        public void TAdd(Page p)
        {
            _pageDal.Insert(p);
        }

        public void TDelete(Page p)
        {
            _pageDal.Delete(p);
        }

        public Page TGetById(int id)
        {
            return _pageDal.GetById(id);
        }

        public Page TGetByUrl(string p)
        {
            return _pageDal.GetByUrl(p);
        }

        public void TUpdate(Page p)
        {
            _pageDal.Update(p);
        }
    }
}
