namespace VehicleCatalog.Service
{
    //VehicleService class - CRUD for Make and Model (Sorting, Filtering & Paging)
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContex context;

        public BaseRepository(ApplicationDbContex contex)
        {
            this.context = contex;
        }

        public void Add(TEntity entity)
        {
            context.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            context.Remove(entity);
        }
    }
}