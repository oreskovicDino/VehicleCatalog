using System;
using System.Threading.Tasks;

namespace VehicleCatalog.Service
{
    public interface IUnitOfWork : IDisposable
    {
        IMakeRepository Makes { get; }
        IModelRepository Models { get; }
        Task<bool> Commit();

    }
}
