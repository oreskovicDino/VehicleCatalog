using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContex context;

        public VehicleService(ApplicationDbContex contex)
        {
            this.context = contex;
        }

        public Make GetMakeById(int id)
        {
            var make = context.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefault();

            return make;
        }
        public Model GetModelById(int id)
        {
            var model = context.Models.Where(m => m.Id == id).FirstOrDefault();

            return model;
        }

        public IEnumerable<Make> GetAllMakes()
        {
            return context.Makes.Include(makes => makes.Models);
        }
        public IEnumerable<Model> GetAllModels()
        {
            return context.Models;
        }

        public IEnumerable<Make> SearchMakes(string searchQuery)
        {
            return context.Makes.Where(m => m.Name.Contains(searchQuery) || m.Abrv.Contains(searchQuery));
        }
        public IEnumerable<Model> SearchModels(string searchQuery)
        {
            return context.Models.Where(
                model => model.Name.Contains(searchQuery)
                ||
                model.Make.Name.Contains(searchQuery)
                ||
                model.Abrv.Contains(searchQuery)
                );
        }

        public void Create<T>(T entity) where T : class
        {
            context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
            
        }

        public void UpdateModelAbrv(int id, string abrv)
        {
            IEnumerable<Model> models = context.Models.Where(m => m.MakeId == id);

            foreach (var model in models)
            {
                model.Abrv = abrv;
                context.Update(model);
            }

            
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

    }
}
