using System.Threading.Tasks;

namespace VehicleCatalog.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContex context;

        public UnitOfWork(ApplicationDbContex context, IMakeRepository makeService, IModelRepository modelService)
        {
            Makes = makeService;
            Models = modelService;
            this.context = context;
        }

        public IMakeRepository Makes { get; set; }

        public IModelRepository Models { get; set; }

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
