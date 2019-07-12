namespace VehicleCatalog.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContex contex;

        public UnitOfWork(ApplicationDbContex contex, IMakeService makeService, IModelService modelService)
        {
            Makes = makeService;
            Models = modelService;
            this.contex = contex;
        }

        public IMakeService Makes { get; set; }

        public IModelService Models { get; set; }

        public void Dispose()
        {
            contex.Dispose();
        }
    }
}
