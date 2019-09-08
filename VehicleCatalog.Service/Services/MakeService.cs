using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using VehicleCatalog.Service.Repositories;
using VehicleCatalog.Service.Repositories.Common;
using VehicleCatalog.Service.Services.Common;
using X.PagedList;

namespace VehicleCatalog.Service.Services
{
    public class MakeService : IMakeService
    {
        #region Fields

        private readonly IMakeRepository makeRepository;
        private readonly IModelRepository modelRepository;

        #endregion

        public MakeService(IMakeRepository makeRepository, IModelRepository modelRepository)
        {
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
        }

        public async Task<IPagedList<Make>> GetPagedMakesAsync(IPagination pagination, ISort sort, IFilter filter)
        {
            return await makeRepository.GetMakesAsync(pagination, sort, filter);
        }

        public async Task<Make> GetMakeAsync(int? id)
        {
            return await makeRepository.GetMakeAsync(id);
        }

        public void Create(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }

            makeRepository.Create(make);
        }

        public void Update(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }
            makeRepository.Update(make);
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Id wasn't valid");
            }

            makeRepository.Delete(id);
        }

        #region Model

        public void UpdateModels(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }

            modelRepository.UpdateModels(make);
        }

        #endregion
    }
}
