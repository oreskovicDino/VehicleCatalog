using System.Threading.Tasks;
using VehicleCatalog.Service.Services.Common;

namespace VehicleCatalog.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContex context;

        public UnitOfWork(ApplicationDbContex context)
        {
            this.context = context;
        }


        public void Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
