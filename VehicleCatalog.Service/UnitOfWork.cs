using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContex contex;

        public UnitOfWork(ApplicationDbContex contex)
        {
            this.contex = contex;
            Makes = new MakeService(contex);
            Models = new ModelService(contex);
        }

        public IMakeService Makes { get; set; }

        public IModelService Models { get; set; }

        public void Dispose()
        {
            contex.Dispose();
        }
    }
}
