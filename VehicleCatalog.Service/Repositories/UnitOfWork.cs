using System.Threading.Tasks;
using VehicleCatalog.Service.Services.Common;

namespace VehicleCatalog.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContex context;

        public IMakeService MakeService { get; }
        public IModelService ModelService { get; }

        public UnitOfWork(ApplicationDbContex context, IMakeService makeService, IModelService modelService)
        {
            this.context = context;
            MakeService = makeService;
            ModelService = modelService;
        }


        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<bool> Commit()
        {
            return (await context.SaveChangesAsync() > 0);
        }
    }
}
