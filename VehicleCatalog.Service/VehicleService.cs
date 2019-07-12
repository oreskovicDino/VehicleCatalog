using System.Threading.Tasks;

namespace VehicleCatalog.Service
{
    //VehicleService class - CRUD for Make and Model (Sorting, Filtering & Paging)
    public class VehicleService<TEntity> : IVehicleService<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContex context;

        public VehicleService(ApplicationDbContex contex)
        {
            this.context = contex;
        }

        public async Task<bool> Add(TEntity entity)
        {
            context.Add(entity);
            return (await context.SaveChangesAsync() > 0);

        }

        public async Task<bool> Remove(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            context.Remove(entity);
            return (await context.SaveChangesAsync() > 0);
        }
    }
}