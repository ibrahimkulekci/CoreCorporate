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
    public class ReferenceManager:IReferenceService
    {
        IReferenceDal _referenceDal;

        public ReferenceManager(IReferenceDal referenceDal)
        {
            _referenceDal = referenceDal;
        }

        public List<Reference> GetList()
        {
            return _referenceDal.GetListAll().OrderByDescending(x => x.ReferenceID).ToList();
        }

        public void TAdd(Reference p)
        {
            _referenceDal.Insert(p);
        }

        public void TDelete(Reference p)
        {
            _referenceDal.Delete(p);
        }

        public Reference TGetById(int id)
        {
            return _referenceDal.GetById(id);
        }

        public Reference TGetByUrl(string p)
        {
            return _referenceDal.GetByUrl(p);
        }

        public void TUpdate(Reference p)
        {
            _referenceDal.Update(p);
        }
    }
}
