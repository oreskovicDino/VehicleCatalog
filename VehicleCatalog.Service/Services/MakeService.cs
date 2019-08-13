using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Repositories;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Service.Services
{
    public class MakeService : IMakeService
    {

        #region Fields

        private readonly GenericRepository<Make> makeReposotory;
        private readonly ApplicationDbContex context;

        #endregion

        public MakeService(ApplicationDbContex context)
        {
            this.makeReposotory = new GenericRepository<Make>(context);
            this.context = context;
        }

        #region Create

        public void Create(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }

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
            if (id == null)
            {
                throw new ArgumentNullException("Id wasn't valid ");
            }
            makeReposotory.Delete(id);
        }

        #endregion

        #region Update

        public void Update(Make makeToUpdate)
        {
            if (makeToUpdate == null)
            {
                throw new ArgumentNullException(nameof(makeToUpdate));
            }
            makeReposotory.Update(makeToUpdate);
        }

        #endregion

        #region GetModelsByMake

        public async Task<IPagedList<Model>> GetModelsByMake(Make make, IPagination pagination)
        {
            if (make == null)
            {
                throw new ArgumentNullException("Something went wrong!!");
            }
            else
            {
                return await make.Models.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 7));
            }

        }

        #endregion
    }
}
