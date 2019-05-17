using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    //VehicleService interface - CRUD for Make and Model (Sorting, Filtering & Paging)
    public interface IVehicleService
    {
        // Selects all records from the Makes table. With paging, sorting, filtering 
        Task<IPagedList<Make>> GetAllMakes(IPagination pagination, ISort sorting, IFilter filter);

        // Selects all records from the Models table. With paging, sorting, filtering
        Task<IPagedList<Model>> GetAllModels(IPagination pagination, ISort sorting, IFilter filter);

        // Selects a single record from the Makes table.
        Task<IMakeToReturn> GetMakeById(int? id);

        // Selects a single record from the Makes table, with paging
        Task<IMakeToReturn> GetMakeById(int? id, IPagination pagin);

        // Selects a single record from the Models table.
        Task<IModelToReturn> GetModelById(int? id);

        // Adds a record to table Makes or Models.
        Task<bool> Create<T>(T entity) where T : class;

        // Updates a record from table Makes.
        Task<bool> UpdateMake(Make make);

        // Updates a record from table Models.
        Task<bool> UpdateModel(Model model);

        // Removes a record from table Makes.
        Task<bool> DeleteMake(int id);

        // Removes a record from table Models.
        Task<bool> DeleteModel(int id);
    }
}
