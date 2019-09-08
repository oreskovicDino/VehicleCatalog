using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Repositories.Common;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Service.Repositories
{
   public class MakeRepository : IMakeRepository 
    {

        #region Fields

        private readonly GenericRepository<Make> makeReposotory;

        #endregion

        public MakeRepository(ApplicationDbContex context, IUnitOfWork unitOfWork)
        {
            this.makeReposotory = new GenericRepository<Make>(context, unitOfWork);
        }

        #region Create

        public void Create(Make make)
        {
            makeReposotory.Create(make);
        }

        #endregion

        #region GetMakesAsync

        public async Task<IPagedList<Make>> GetMakesAsync(IPagination pagination, ISort sort, IFilter filter)
        {
            return await makeReposotory.GetPagedList(
                (IQueryable<Make> query) =>
                {
                    if (!String.IsNullOrEmpty(filter.FilterString))
                    {
                        query = query.Where(m => m.Name.Contains(filter.FilterString) || m.Abrv.Contains(filter.FilterString));
                    }

                    switch (sort.Sorting)
                    {
                        case "NameDesc":
                            return query.OrderByDescending(o => o.Name);
                        case "AbrvAsc":
                            return query.OrderBy(o => o.Abrv);
                        case "AbrvDesc":
                            return query.OrderByDescending(o => o.Abrv);
                        default:
                            return query.OrderBy(o => o.Name);
                    }
                },
                pagination
                );
        }

        #endregion

        #region GetMakeAsync

        public async Task<Make> GetMakeAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await makeReposotory.GetSingleItem
                (
                    (IQueryable<Make> query) =>
                    {
                        return query.Where(m => m.Id == id).Include(m => m.Models);
                    }
                );
        }

        #endregion

        #region Delete

        public void Delete(int? id)
        {
            makeReposotory.Delete(id);
        }

        #endregion

        #region Update

        public void Update(Make makeToUpdate)
        {
            makeReposotory.Update(makeToUpdate);
        }

        #endregion
    }
}
