using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    public interface IMakeRepository : IBaseRepository<Make>
    {
        // Selects all records from the Makes table. With paging, sorting, filtering 
        Task<IPagedList<Make>> GetAll(IPagination pagination, ISort sorting, IFilter filter);

        // Selects a single record from the Makes table.
        Task<Make> GetById(int? id);

        // Updates a record from table Makes.
        //Task<bool> Update(Make make);
        void Update(Make make);
    }
}

