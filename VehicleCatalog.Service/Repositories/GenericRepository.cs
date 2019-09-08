using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Service.Models;
using X.PagedList;

namespace VehicleCatalog.Service.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Fields

        internal ApplicationDbContex context;
        private readonly IUnitOfWork unitOfWork;
        internal DbSet<TEntity> dbSet;

        #endregion

        public GenericRepository(ApplicationDbContex context, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.dbSet = context.Set<TEntity>();
        }

        #region Create

        public void Create(TEntity entity)
        {
            dbSet.Add(entity);
            //context.SaveChanges();
            unitOfWork.Save();
        }

        #endregion

        #region GetPagedList

        public async Task<IPagedList<TEntity>> GetPagedList(Func<IQueryable<TEntity>, IQueryable<TEntity>> sortOrder, IPagination pagination)
        {
            IQueryable<TEntity> query = dbSet;

            query = sortOrder(query);

            return await query.ToPagedListAsync((pagination.CurrentPage ?? 1), (pagination.Size ?? 5));
        }

        #endregion

        #region GetSingle

        public async Task<TEntity> GetSingleItem(Func<IQueryable<TEntity>, IQueryable<TEntity>> whereQuery)
        {
            IQueryable<TEntity> query = dbSet;

            query = whereQuery(query);

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region Update

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
            unitOfWork.Save();
        }

        public void UpdateRange(Action<IQueryable<TEntity>, DbSet<TEntity>> delegateQuery)
        {
            IQueryable<TEntity> query = dbSet;
            delegateQuery(query, dbSet);
            //context.SaveChanges();
            unitOfWork.Save();
        }

        #endregion

        #region Delete

        public void Delete(int? id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        private void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            unitOfWork.Save();
        }

        #endregion
    }
}
