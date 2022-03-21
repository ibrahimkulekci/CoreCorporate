using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ISettingService:IGenericService<Setting>
    {
        public Setting GetByKey(string key);
        Setting TGetByKey(string p);
    }
}
