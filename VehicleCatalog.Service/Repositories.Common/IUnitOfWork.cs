using System;
using System.Threading.Tasks;
using VehicleCatalog.Service.Services.Common;

namespace VehicleCatalog.Service
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
