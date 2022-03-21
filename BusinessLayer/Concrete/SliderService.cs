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
    public class SliderService:ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public List<Slider> GetList()
        {
            return _sliderRepository.GetListAll().OrderByDescending(x => x.ID).ToList();
        }

        public void TAdd(Slider p)
        {
            _sliderRepository.Insert(p);
        }

        public void TDelete(Slider p)
        {
            _sliderRepository.Delete(p);
        }

        public Slider TGetById(int id)
        {
            return _sliderRepository.GetById(id);
        }

        public Slider TGetByUrl(string p)
        {
            return _sliderRepository.GetByUrl(p);
        }

        public void TUpdate(Slider p)
        {
            _sliderRepository.Update(p);
        }
    }
}
