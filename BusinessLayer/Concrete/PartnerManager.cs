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
    public class PartnerManager:IPartnerService
    {
        IPartnerDal _partnerDal;

        public PartnerManager(IPartnerDal partnerDal)
        {
            _partnerDal = partnerDal;
        }

        public List<Partner> GetList()
        {
            return _partnerDal.GetListAll().OrderByDescending(x => x.PartnerID).ToList();
        }

        public void TAdd(Partner p)
        {
            _partnerDal.Insert(p);
        }

        public void TDelete(Partner p)
        {
            _partnerDal.Delete(p);
        }

        public Partner TGetById(int id)
        {
            return _partnerDal.GetById(id);
        }

        public Partner TGetByUrl(string p)
        {
            return _partnerDal.GetByUrl(p);
        }

        public void TUpdate(Partner p)
        {
            _partnerDal.Update(p);
        }
    }
}
