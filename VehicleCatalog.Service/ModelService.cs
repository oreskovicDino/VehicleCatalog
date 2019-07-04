using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service
{
    public class ModelService : VehicleService<Model>, IModelService
    {
        private readonly ApplicationDbContex context;

        public ModelService(ApplicationDbContex context) : base(context)
        {
            this.context = context;
        }

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

        public async Task<IPagedList<Model>> GetModelsByMake(Make make, IPagination pagination)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make));
            }

            IPagedList<Model> modelByMake = await make.Models.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 7));
            return modelByMake;
        }

        public async Task<bool> Update(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            //using (context)
            //{
            context.Update(model);
            return (await context.SaveChangesAsync() > 0);
            //}
        }

        public async Task<Model> GetModelForDetail(int? id)
        {
            Model model = await context.Models.Where(mod => mod.Id == id).FirstOrDefaultAsync();
            return model;
        }

    }
}
