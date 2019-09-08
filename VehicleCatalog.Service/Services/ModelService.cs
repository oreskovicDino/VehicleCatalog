using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Repositories;
using VehicleCatalog.Service.Repositories.Common;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Service.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository modelRepository;
        private readonly IMakeRepository makeRepository;

        #region Fields


        #endregion

        public ModelService(IModelRepository modelRepository, IMakeRepository makeRepository)
        {
            this.modelRepository = modelRepository;
            this.makeRepository = makeRepository;
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
            return await modelRepository.GetModelsAsync(pagination, sort, filter);
        }

        #endregion

        #region GetModelAsync

        public async Task<Model> GetModelAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await modelRepository.GetModelAsync(id);
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

            modelRepository.UpdateModels(make);
           }

        #endregion

        #region Make

        public async Task<Make> GetMakeAsync(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await makeRepository.GetMakeAsync(id);
        }

        public async Task<IPagedList<Make>> GetMakesAsync(IPagination pagination, ISort sort, IFilter filter)
        {
            return await makeRepository.GetMakesAsync(pagination, sort, filter);
        }
        
        #endregion
    }
}
