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
    public class TeamManager:ITeamService
    {
        ITeamDal _teamDal;

        public TeamManager(ITeamDal teamDal)
        {
            _teamDal = teamDal;
        }

        public List<Team> GetList()
        {
            return _teamDal.GetListAll().OrderByDescending(x => x.TeamID).ToList();
        }

        public void TAdd(Team p)
        {
            _teamDal.Insert(p);
        }

        public void TDelete(Team p)
        {
            _teamDal.Delete(p);
        }

        public Team TGetById(int id)
        {
            return _teamDal.GetById(id);
        }

        public Team TGetByUrl(string p)
        {
            return _teamDal.GetByUrl(p);
        }

        public void TUpdate(Team p)
        {
            _teamDal.Update(p);
        }
    }
}
