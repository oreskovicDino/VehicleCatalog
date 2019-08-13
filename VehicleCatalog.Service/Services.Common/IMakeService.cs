using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service.Services.Common
{
    public interface IMakeService
    {
        void Create(Make make);

        Task<IPagedList<Make>> GetMakesAsync(IPagination pagination, ISort sort, IFilter flter);

        Task<Make> GetMakeAsync(int? id);

        void Delete(int? id);

        void Update(Make makeToUpdate);

        Task<IPagedList<Model>> GetModelsByMake(Make make, IPagination pagination);

    }
}
