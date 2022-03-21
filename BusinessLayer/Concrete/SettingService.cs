using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SettingService:ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public Setting GetByKey(string key)
        {
            var values = _settingRepository.GetListAll().Where(x => x.Key == key).FirstOrDefault();
            return values;
        }

        public List<Setting> GetList()
        {
            return _settingRepository.GetListAll();
        }

        public void TAdd(Setting p)
        {
            _settingRepository.Insert(p);
        }

        public void TDelete(Setting p)
        {
            _settingRepository.Delete(p);
        }

        public Setting TGetById(int id)
        {
            return _settingRepository.GetById(id);
        }

        public Setting TGetByKey(string p)
        {
            return _settingRepository.GetListAll().Where(x => x.Key == p).FirstOrDefault();
        }

        public Setting TGetByUrl(string p)
        {
            return _settingRepository.GetByUrl(p);
        }

        public void TUpdate(Setting p)
        {
            _settingRepository.Update(p);
        }
    }
}
