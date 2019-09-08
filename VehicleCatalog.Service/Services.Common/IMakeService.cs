using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service.Services.Common
{
    public interface IMakeService
    {
        Task<IPagedList<Make>> GetPagedMakesAsync(IPagination pagination, ISort sort, IFilter filter);

        Task<Make> GetMakeAsync(int? id);

        void Create(Make make);

        void Update(Make make);

        void UpdateModels(Make make);

        void Delete(int? id);
    }
}
