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
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetList()
        {
            return _userRepository.GetListAll().OrderByDescending(x => x.ID).ToList();
        }

        public void TAdd(User p)
        {
            _userRepository.Insert(p);
        }

        public void TDelete(User p)
        {
            _userRepository.Delete(p);
        }

        public User TGetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User TGetByUrl(string p)
        {
            return _userRepository.GetByUrl(p);
        }

        public void TUpdate(User p)
        {
            _userRepository.Update(p);
        }
    }
}
