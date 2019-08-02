namespace VehicleCatalog.Service
{
    //VehicleService interface - CRUD for Make and Model (Sorting, Filtering & Paging)
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
