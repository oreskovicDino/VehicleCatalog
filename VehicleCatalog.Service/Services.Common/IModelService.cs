using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service.Services.Common
{
    public interface IModelService
    {
        void Create(Model model);

        Task<IPagedList<Model>> GetModelsAsync(IPagination pagination, ISort sort, IFilter filter);

        Task<Model> GetModelAsync(int? id);

        void Delete(int? id);

        void Update(Model model);

        void UpdateModels(Make make);
    }
}
