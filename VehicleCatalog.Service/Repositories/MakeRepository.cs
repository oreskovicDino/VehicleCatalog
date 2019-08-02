using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    public class MakeRepository : BaseRepository<Make>, IMakeRepository
    {
        private readonly ApplicationDbContex context;

        public MakeRepository(ApplicationDbContex context) : base(context)
        {
            this.context = context;
        }

        #region GetAll

        //<summary>GetAll</summary>
        // Selects all records from the Makes table. With paging, sorting, filtering 
        public async Task<IPagedList<Make>> GetAll(IPagination pagination, ISort sorting, IFilter filter)
        {
            var query = context.Makes.AsQueryable();

            if (!String.IsNullOrEmpty(filter.FilterString))
            {
                query = query.Where(m => m.Name.Contains(filter.FilterString) || m.Abrv.Contains(filter.FilterString));
            }

            switch (sorting.Sorting)
            {

                case "NameDesc":
                    query = query.OrderByDescending(o => o.Name);
                    break;
                case "AbrvAsc":
                    query = query.OrderBy(o => o.Abrv);
                    break;
                case "AbrvDesc":
                    query = query.OrderByDescending(o => o.Abrv);
                    break;
                default:
                    query = query.OrderBy(o => o.Name);
                    break;

            }

            IPagedList<Make> make = await query.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 5));
            return make;
        }
        #endregion

        #region GetById

        // Selects a single record from the Makes table.
        public async Task<Make> GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }


            Make make = await context.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefaultAsync();
            return make;

        }
        #endregion

        #region Update

        // Updates a single record in table Make
        public void Update(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }
            context.Update(make);
            IEnumerable<Model> models = context.Models.Where(m => m.MakeId == make.Id);
            foreach (var model in models)
            {
                model.Abrv = make.Abrv;
                context.Update(model);
            }
        }
        #endregion
    }
}
