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
    public class MakeService : VehicleService<Make>, IMakeService
    {
        private readonly ApplicationDbContex context;

        public MakeService(ApplicationDbContex context) : base(context)
        {
            this.context = context;
        }

        // Selects all records from the Makes table. With paging, sorting, filtering 
        public async Task<IPagedList<Make>> GetAll(IPagination pagination, ISort sorting, IFilter filter)
        {

            //IPagedList<Make> make = await query.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 5));
            //return make;

            //using (context)
            //{
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

            //}
        }

        // Selects a single record from the Makes table.
        public async Task<Make> GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            //using (context)
            //{
            Make make = await context.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefaultAsync();
            return make;
            //}
        }

        // Updates a record from table Makes.
        public async Task<bool> Update(Make make)
        {

            //using (context)
            //{
            context.Update(make);

            IEnumerable<Model> models = context.Models.Where(m => m.MakeId == make.Id);

            foreach (var model in models)
            {
                model.Abrv = make.Abrv;
                context.Update(model);
            }

            return (await context.SaveChangesAsync() > 0);

            //}
        }



        //// Selects a single record from the Makes table, with paging
        //public async Task<IMakeToReturn> GetById(int? id, IPagination pagination)
        //{
        //    if (id == null)
        //    {
        //        throw new ArgumentNullException(nameof(id));
        //    }

        //    using (context)
        //    {
        //        Make make = await context.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefaultAsync();

        //        IMakeToReturn makeToReturn = new MakeToReturn
        //        {
        //            MakeByID = make,
        //            ModelsByMake = await make.Models.ToPagedListAsync(pagination.CurrentPage ?? 1, pagination.Size ?? 5)
        //        };

        //        return makeToReturn;
        //    }
        //}

        //// Adds a record to table Makes
        //public async Task<bool> Create(Make make)
        //{
        //    if (make == null)
        //    {
        //        throw new ArgumentNullException(nameof(make));
        //    }

        //    using (context)
        //    {
        //        context.Add(make);
        //        return (await context.SaveChangesAsync() > 0);
        //    }
        //}

        //// Removes a record from table Makes.
        //public async Task<bool> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        throw new ArgumentNullException(nameof(id));
        //    }

        //    using (context)
        //    {
        //        Make make = await context.Makes.FindAsync(id);
        //        if (make == null)
        //        {
        //            return false;
        //        }
        //        context.Remove(make);
        //        return (await context.SaveChangesAsync() > 0);
        //    }
        //}

        // Updates a record from table Makes.
    }
}
