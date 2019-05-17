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
    //VehicleService class - CRUD for Make and Model (Sorting, Filtering & Paging)
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContex context;
        private readonly IMapper mapper;

        public VehicleService(ApplicationDbContex contex, IMapper mapper)
        {
            this.context = contex;
            this.mapper = mapper;
        }

        // Selects all records from the Makes table. With paging, sorting, filtering 
        public async Task<IPagedList<Make>> GetAllMakes(IPagination pagination, ISort sorting, IFilter filter)
        {
            using (context)
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
        }

        // Selects all records from the Models table. With paging, sorting and filtering by Model or Make
        public async Task<IPagedList<Model>> GetAllModels(IPagination pagination, ISort sorting, IFilter filter)
        {
            using (context)
            {
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
            }
        }

 
        // Selects a single record from the Makes table.
        public async Task<IMakeToReturn> GetMakeById(int? id)
        {
            using (context)
            {
                Make make = await context.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefaultAsync();

                IMakeToReturn makeToReturn = new MakeToReturn
                {
                    MakeByID = make
                };

                return makeToReturn;
            }
        }

        // Selects a single record from the Makes table, with paging
        public async Task<IMakeToReturn> GetMakeById(int? id, IPagination pagination)
        {
            using (context)
            {
                Make make = await context.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefaultAsync();

                IMakeToReturn makeToReturn = new MakeToReturn
                {
                    MakeByID = make,
                    ModelsByMake = await make.Models.ToPagedListAsync(pagination.CurrentPage ?? 1, pagination.Size ?? 5)
                };

                return makeToReturn;
            }
        }

        // Selects a single record from the Models table.
        public async Task<IModelToReturn> GetModelById(int? id)
        {
            using (context)
            {
                Model model = await context.Models.Where(mod => mod.Id == id).FirstOrDefaultAsync();

                IModelToReturn modelToReturn = new ModelToReturn
                {
                    ModelByID = model,
                    MakeByID = await context.Makes.Where(m => m.Id == model.MakeId).FirstOrDefaultAsync()
                };
                return modelToReturn;
            }
        }

        // Adds a record to table Makes or Models.
        public async Task<bool> Create<T>(T entity) where T : class
        {
            using (context)
            {
                context.Add(entity);
                return (await context.SaveChangesAsync() > 0);
            }
        }

        // Updates a record from table Makes.
        public async Task<bool> UpdateMake(Make make)
        {
            using (context)
            {
                context.Update(make);

                IEnumerable<Model> models = context.Models.Where(m => m.MakeId == make.Id);

                foreach (var model in models)
                {
                    model.Abrv = make.Abrv;
                    context.Update(model);
                }

                return (await context.SaveChangesAsync() > 0);

            }
        }

        // Updates a record from table Models.
        public async Task<bool> UpdateModel(Model model)
        {
            using (context)
            {
                context.Update(model);
                return (await context.SaveChangesAsync() > 0);
            }
        }

        // Removes a record from table Makes.
        public async Task<bool> DeleteMake(int id)
        {
            using (context)
            {
                Make make = await context.Makes.FindAsync(id);
                if (make == null)
                {
                    return false;
                }
                context.Remove(make);
                return (await context.SaveChangesAsync() > 0);
            }

        }

        // Removes a record from table Models.
        public async Task<bool> DeleteModel(int id)
        {
            using (context)
            {
                Model model = await context.Models.FindAsync(id);
                if (model == null)
                {
                    return false;
                }
                context.Remove(model);
                return (await context.SaveChangesAsync() > 0);
            }
        }
    }
}
