using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;


namespace VehicleCatalog.Service
{
    public interface IVehicleService
    {
        Make GetMakeById(int id);
        Model GetModelById(int id);

        IEnumerable<Make> GetAllMakes();
        IEnumerable<Make> SearchMakes(string searchQuery);
        IEnumerable<Model> GetAllModels();
        IEnumerable<Model> SearchModels(string searchQuery);


        Task CreateMake(Make make);
        Task CreateModel(Model model);

        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void UpdateModelAbrv(int id, string abrv);

        Task<bool> SaveAll();
    }
}
