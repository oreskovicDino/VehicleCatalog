using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace VehicleCatalog.Service.Models
{
    // Interface for returning record from the Makes table and paged list from the Models table
    public interface IMakeToReturn
    {
        // Record from the Makes table
        Make MakeByID { get; set; }
        // Paged list from the Models table
        IPagedList<Model> ModelsByMake { get; set; }
    }
}
