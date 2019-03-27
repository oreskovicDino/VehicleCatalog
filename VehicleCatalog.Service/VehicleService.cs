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
        private readonly ApplicationDbContex contex;

        public VehicleService(ApplicationDbContex contex)
        {
            this.contex = contex;
        }

        public Task CreateMake(Make make)
        {
            throw new NotImplementedException();
        }

        public Task CreateModel(Model model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMake(int makeId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteModel(int modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Make> GetAllMakes()
        {
            return contex.Makes.Include(makes => makes.Models);
        }

        public IEnumerable<Model> GetAllModels()
        {
            throw new NotImplementedException();
        }

        public Make GetMakeById(int id)
        {
            var make = contex.Makes.Where(m => m.Id == id).Include(m => m.Models).FirstOrDefault();

            return make;
        }

        public Model GetModelById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
