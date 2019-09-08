using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service.Repositories.Common
{
    public interface IModelRepository
    {
        void Create(Model model);

        Task<IPagedList<Model>> GetModelsAsync(IPagination pagination, ISort sort, IFilter filter);

        Task<Model> GetModelAsync(int? id);

        void Delete(int? id);

        void Update(Model model);

        void UpdateModels(Make make);
    }
}
