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
            CreateMap<Make, MakeForCreationDto>().ReverseMap();
            CreateMap<Make, MakeForDetailDto>().ReverseMap();
            CreateMap<Make, MakeForModelDto>().ReverseMap();
            CreateMap<Make, MakeForUpdateDto>().ReverseMap();

            CreateMap<Model, ModelForCreationDto>().ReverseMap();
            CreateMap<Model, ModelForDetailDto>().ReverseMap();
            CreateMap<Model, ModelForUpdateDto>().ReverseMap();
        }
    }
}
