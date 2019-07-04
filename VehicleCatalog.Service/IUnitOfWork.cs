using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service
{
    public interface IUnitOfWork : IDisposable
    {
        IMakeService Makes { get; }
        IModelService Models { get; }
    }
}
