using System;

namespace VehicleCatalog.Service
{
    public interface IUnitOfWork : IDisposable
    {
        IMakeService Makes { get; }
        IModelService Models { get; }
    }
}
