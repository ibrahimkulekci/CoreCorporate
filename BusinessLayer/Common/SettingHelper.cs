using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Common
{

    public class SettingHelper
    {
        public static string GetValue(string key)
        {
            SettingService pm = new SettingService(new EfSettingRepository(new AppDbContext()));

            string value = pm.GetByKey(key).Value;
            return value;
        }
    }
}
