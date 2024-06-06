using Microsoft.EntityFrameworkCore.Diagnostics;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;

namespace NZWalk_API.Repositories.Interface
{
    public interface IRegionRepository
    {
        Task<List<RegionDto>> GetAllAsync();
        Task<RegionDto> GetById(Guid Id);
        Task<RegionDto> CreateAsync(AddRegionRequestDTO addRegionRequestDTO);
        Task<RegionDto> UpdateAsync(Guid Id,UpdateResionRequestDTO updateResionRequestDTO);
        Task<RegionDto> DeleteAsync(Guid Id);
        Task SaveAsync();
        


    }
}
