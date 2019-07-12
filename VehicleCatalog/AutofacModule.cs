using Autofac;
using VehicleCatalog.Service;

namespace VehicleCatalog
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Registering UnitOfWork
            builder.Register(c => new UnitOfWork(c.Resolve<ApplicationDbContex>(), c.Resolve<IMakeService>(), c.Resolve<IModelService>())).As<IUnitOfWork>().InstancePerLifetimeScope();
            //Registering MakeService
            builder.Register(c => new MakeService(c.Resolve<ApplicationDbContex>())).As<IMakeService>().InstancePerLifetimeScope();
            //Registering ModelService
            builder.Register(c => new ModelService(c.Resolve<ApplicationDbContex>())).As<IModelService>().InstancePerLifetimeScope();
        }

    }
}
