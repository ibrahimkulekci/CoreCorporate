using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Team;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public BaseResultListModel<Team> GetAllByQuery(TeamQueryModel queryModel)
        {
            BaseResultListModel<Team> resultList = new BaseResultListModel<Team>()
            {
                DataList = new List<Team>(),
                TotalRecordCount = 0
            };
            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from n in dbContext.Teams
                              select n);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(x => x.TeamID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(x => x.TeamStatus == queryModel.Filter_IsActive);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.TeamCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.TeamCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.TeamName.Contains(queryModel.Filter_Search));
                }

                //shorting
                if (!string.IsNullOrEmpty(queryModel.SortOn) && !string.IsNullOrEmpty(queryModel.SortDirection))
                {
                    record = record.OrderBy(string.Format("{0} {1}", queryModel.SortOn, queryModel.SortDirection));
                }

                //total count
                resultList.TotalRecordCount = record.Count();

                //paging
                record = record.Skip(queryModel.PageSize * (queryModel.CurrentPage - 1)).Take(queryModel.PageSize);

                resultList.DataList.AddRange(record.AsNoTracking().ToList());
            }

            return resultList;
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
