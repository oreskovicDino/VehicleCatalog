using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service.Repositories.Common
{
    public interface IMakeRepository
    {
        void Create(Make make);

        Task<IPagedList<Make>> GetMakesAsync(IPagination pagination, ISort sort, IFilter flter);

        Task<Make> GetMakeAsync(int? id);

        void Delete(int? id);

        void Update(Make makeToUpdate);

    }
}
