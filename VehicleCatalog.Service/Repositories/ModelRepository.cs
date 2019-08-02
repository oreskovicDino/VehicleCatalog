using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    public class ModelRepository : BaseRepository<Model>, IModelRepository
    {
        private readonly ApplicationDbContex context;

        public ModelRepository(ApplicationDbContex context) : base(context)
        {
            this.context = context;
        }

        #region GetAll

        public async Task<IPagedList<Model>> GetAll(IPagination pagination, ISort sorting, IFilter filter)
        {
            //using (context)
            //{
            var query = context.Models.AsQueryable();

            if (!String.IsNullOrEmpty(filter.FilterString))
            {
                query = query.Where(m =>
                m.Name.Contains(filter.FilterString)
                ||
                m.Abrv.Contains(filter.FilterString)
                ||
                m.Make.Name.Contains(filter.FilterString)
                );
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

            IPagedList<Model> model = await query.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 7));
            return model;
            //}
        }

        #endregion

        #region GetById
        public async Task<Model> GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            //using (context)
            //{
            Model model = await context.Models.Where(mod => mod.Id == id).FirstOrDefaultAsync();
            return model;
            //}
        }
        #endregion

        #region GetModelsByMake
        public async Task<IPagedList<Model>> GetModelsByMake(Make make, IPagination pagination)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }

            IPagedList<Model> modelByMake = await make.Models.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 7));
            return modelByMake;
        }
        #endregion

        #region Update
        public void Update(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            context.Update(model);
        }
        #endregion

    }
}
