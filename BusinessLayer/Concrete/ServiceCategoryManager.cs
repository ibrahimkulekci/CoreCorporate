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
    public class ServiceCategoryManager:IServiceCategoryService
    {
        IServiceCategoryDal _serviceCategoryDal;

        public ServiceCategoryManager(IServiceCategoryDal serviceCategoryDal)
        {
            _serviceCategoryDal = serviceCategoryDal;
        }

        public List<ServiceCategory> GetList()
        {
            return _serviceCategoryDal.GetListAll().OrderByDescending(x => x.ServiceCategoryID).ToList();
        }

        public void TAdd(ServiceCategory p)
        {
            _serviceCategoryDal.Insert(p);
        }

        public void TDelete(ServiceCategory p)
        {
            _serviceCategoryDal.Delete(p);
        }

        public ServiceCategory TGetById(int id)
        {
            return _serviceCategoryDal.GetById(id);
        }

        public ServiceCategory TGetByUrl(string p)
        {
            return _serviceCategoryDal.GetByUrl(p);
        }

        public void TUpdate(ServiceCategory p)
        {
            _serviceCategoryDal.Update(p);
        }
    }
}
