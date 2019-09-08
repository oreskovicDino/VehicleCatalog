using Autofac;
using VehicleCatalog.Service;
using VehicleCatalog.Service.Services.Common;

namespace VehicleCatalog
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Registering UnitOfWork
            builder.Register(c => new UnitOfWork(c.Resolve<ApplicationDbContex>())).As<IUnitOfWork>().InstancePerLifetimeScope();
            //Registering MakeService
            //builder.Register(c => new MakeRepository(c.Resolve<ApplicationDbContex>())).As<IMakeRepository>().InstancePerLifetimeScope();
            //Registering ModelService
            //builder.Register(c => new ModelRepository(c.Resolve<ApplicationDbContex>())).As<IModelRepository>().InstancePerLifetimeScope();
        }

    }
}
