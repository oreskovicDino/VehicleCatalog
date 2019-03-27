using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Service
{
    public interface IVehicleService
    {
        Make  GetMakeById(int id);
        Model GetModelById(int id);

        IEnumerable<Make> GetAllMakes();
        IEnumerable<Model> GetAllModels();

        Task CreateMake(Make make);
        Task CreateModel(Model model);

        Task DeleteMake(int makeId);
        Task DeleteModel(int modelId);
                       
    }
}
