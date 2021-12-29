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
    public class ServiceManager:IServiceService
    {
        IServiceDal _serviceDal;

        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public List<Service> GetList()
        {
            return _serviceDal.GetListAll().OrderByDescending(x => x.ServiceID).ToList();
        }

        public void TAdd(Service p)
        {
            _serviceDal.Insert(p);
        }

        public void TDelete(Service p)
        {
            _serviceDal.Delete(p);
        }

        public Service TGetById(int id)
        {
            return _serviceDal.GetById(id);
        }

        public Service TGetByUrl(string p)
        {
            return _serviceDal.GetByUrl(p);
        }

        public void TUpdate(Service p)
        {
            _serviceDal.Update(p);
        }
    }
}
