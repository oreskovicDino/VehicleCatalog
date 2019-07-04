using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    public interface IMakeService : IVehicleService<Make>
    {
        // Selects all records from the Makes table. With paging, sorting, filtering 
        Task<IPagedList<Make>> GetAll(IPagination pagination, ISort sorting, IFilter filter);

        // Selects a single record from the Makes table.
        Task<Make> GetById(int? id);

        // Updates a record from table Makes.
        Task<bool> Update(Make make);





        //// Selects a single record from the Makes table, with paging
        //Task<IMakeToReturn> GetById(int? id, IPagination pagination);

        //// Removes a record from table Makes.
        //Task<bool> Delete(int? id);
 
        //// Adds a record to table Makes
        //Task<bool> Create(Make make);
    }
}

