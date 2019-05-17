using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace VehicleCatalog.Service.Models
{
    // Class for returning record from the Makes table and paged list from the Models table
    public class MakeToReturn : IMakeToReturn
    {
        // Record from the Makes table
        public Make MakeByID { get; set; }
        // Paged list from the Models table
        public IPagedList<Model> ModelsByMake { get; set; }
    }
}
