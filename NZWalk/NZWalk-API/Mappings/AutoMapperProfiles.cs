using AutoMapper;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
using System.Globalization;

namespace NZWalk_API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDTO,Region>().ReverseMap();
            CreateMap<AddRegionRequestDTO, RegionDto>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO,Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();


        }
    }
}
