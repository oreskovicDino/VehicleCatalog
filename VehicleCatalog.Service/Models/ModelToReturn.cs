using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service.Models
{
    // Class for returning record from the Models table and record from Makes table
    class ModelToReturn : IModelToReturn
    {
        // Record from the Models table
        public Model ModelByID { get; set; }
        // Record from the Makes table
        public Make MakeByID {   get; set; }
    }
}
