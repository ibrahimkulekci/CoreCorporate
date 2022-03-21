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
    public class MenuService:IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public List<Menu> GetList()
        {
            return _menuRepository.GetListAll().OrderByDescending(x => x.MenuID).ToList();
        }

        public void TAdd(Menu p)
        {
            _menuRepository.Insert(p);
        }

        public void TDelete(Menu p)
        {
            _menuRepository.Delete(p);
        }

        public Menu TGetById(int id)
        {
            return _menuRepository.GetById(id);
        }

        public Menu TGetByUrl(string p)
        {
            return _menuRepository.GetByUrl(p);
        }

        public void TUpdate(Menu p)
        {
            _menuRepository.Update(p);
        }
    }
}
