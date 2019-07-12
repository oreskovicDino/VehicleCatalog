using System.Threading.Tasks;

namespace VehicleCatalog.Service
{
    //VehicleService interface - CRUD for Make and Model (Sorting, Filtering & Paging)
    public interface IVehicleService<TEntity> where TEntity : class
    {
        // Adds a record to table Makes or Models.
        Task<bool> Add(TEntity entity);

        // Removes a record from table Makes or Models.
        Task<bool> Remove(TEntity entity);

    }
}
