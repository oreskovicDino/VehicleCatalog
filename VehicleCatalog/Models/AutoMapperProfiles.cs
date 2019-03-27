using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCatalog.Models.MakeDtos;
using VehicleCatalog.Models.ModelDto;
using VehicleCatalog.Service.Models;

namespace VehicleCatalog.Models
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Make, MakeForListDto>().ReverseMap();
            CreateMap<Make, ModelListForMakeDto>().ReverseMap();

        }
    }
}
