using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service.Models
{
    // Interface for returning record from the Models table and record from Makes table
    public interface IModelToReturn
    {
        // Record from the Models table
        Model ModelByID { get; set; }
        // Record from the Makes table
        Make MakeByID { get; set; }
    }
}
