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
    public class ContactService:IContactService
    {
        private readonly IContactRepository _contactService;

        public ContactService(IContactRepository contactService)
        {
            _contactService = contactService;
        }

        public List<Contact> GetList()
        {
            return _contactService.GetListAll();
        }

        public void TAdd(Contact p)
        {
            _contactService.Insert(p);
        }

        public void TDelete(Contact p)
        {
            _contactService.Delete(p);
        }

        public Contact TGetById(int id)
        {
            return _contactService.GetById(id);
        }

        public Contact TGetByUrl(string p)
        {
            return _contactService.GetByUrl(p);
        }

        public void TUpdate(Contact p)
        {
            _contactService.Update(p);
        }
    }
}
