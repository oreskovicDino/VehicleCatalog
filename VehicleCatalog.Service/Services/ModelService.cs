using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Repositories;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Service.Services
{
    public class ModelService : IModelService
    {
        #region Fields

        private readonly GenericRepository<Model> modelRepository;

        #endregion

        public ModelService(ApplicationDbContex context)
        {
            this.modelRepository = new GenericRepository<Model>(context);
        }

        #region Create

        public void Create(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            modelRepository.Create(model);
        }

        #endregion

        #region GetModelsAsync

        public async Task<IPagedList<Model>> GetModelsAsync(IPagination pagination, ISort sort, IFilter filter)
        {
            return await modelRepository.GetPagedList(
                (IQueryable<Model> query) =>
                    {
                        if (!String.IsNullOrEmpty(filter.FilterString))
                        {
                            query = query.Where(
                                m => m.Name.Contains(filter.FilterString)
                                ||
                                m.Abrv.Contains(filter.FilterString)
                                ||
                                m.Make.Name.Contains(filter.FilterString)
                                );
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

        #region GetModelAsync

        public async Task<Model> GetModelAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await modelRepository.GetSingleItem(

                (IQueryable<Model> query) =>
                {
                    return query.Where(m => m.Id == id);
                }

                );
        }

        #endregion

        #region Delete

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Id wasn't valid");
            }

            modelRepository.Delete(id);
        }

        #endregion

        #region Update

        public void Update(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            modelRepository.Update(model);
        }


        public void UpdateModels(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }

            modelRepository.UpdateRange
                (
                    (IQueryable<Model> query, DbSet<Model> dbSet) =>
                    {
                        IEnumerable<Model> TCollection = query.Where(m => m.MakeId == make.Id);

                        foreach (var TItem in TCollection)
                        {
                            TItem.Abrv = make.Abrv;
                            dbSet.Update(TItem);
                        }
                    }
                );
        }

        #endregion

    }
}
