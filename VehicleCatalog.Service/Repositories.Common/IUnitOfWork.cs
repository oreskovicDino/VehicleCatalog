using System;
using System.Threading.Tasks;
using VehicleCatalog.Service.Services.Common;

namespace VehicleCatalog.Service
{
    public interface IUnitOfWork : IDisposable
    {
        IMakeService MakeService { get; }
        IModelService ModelService { get; }
        Task<bool> Commit();

    }
}
