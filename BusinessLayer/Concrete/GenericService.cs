using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly GenericRepository<T> _genericRepository;

        public GenericService(GenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        
        public List<T> GetList()
        {
            return _genericRepository.GetListAll(); //.OrderByDescending(x => x.GalleryID).ToList();
        }

        public void TAdd(T p)
        {
            _genericRepository.Insert(p);
        }

        public void TDelete(T p)
        {
            _genericRepository.Delete(p);
        }

        public T TGetById(int id)
        {
            return _genericRepository.GetById(id);
        }

        public T TGetByUrl(string p)
        {
            return _genericRepository.GetByUrl(p);
        }

        public void TUpdate(T p)
        {
            _genericRepository.Update(p);
        }
    }
}
