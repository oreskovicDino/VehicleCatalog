using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    public interface IModelService : IVehicleService<Model>
    {
        // Selects all records from the Models table. With paging, sorting, filtering
        Task<IPagedList<Model>> GetAll(IPagination pagination, ISort sorting, IFilter filter);

        // Selects a single record from the Models table.
        Task<Model> GetById(int? id);

        //Selects all Models that belong to one Make.
        Task<IPagedList<Model>> GetModelsByMake(Make make, IPagination pagination);

        // Updates a record from table Models.
        Task<bool> Update(Model model);

        Task<Model> GetModelForDetail(int? id);







        //// Adds a record to table Makes or Models.
        //Task<bool> Create(Model model);

        //// Removes a record from table Models.
        //Task<bool> Delete(int? id);
    }
}
