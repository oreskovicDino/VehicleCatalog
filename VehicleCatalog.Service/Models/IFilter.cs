using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCatalog.Service.Models
{
    // Interface for searching and filtering 
    public interface IFilter
    {
        // Keyword for searching tables
        string FilterString { get; set; }
    }
}
