using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;

namespace NZWalk_API.Repositories.Interface
{
    public interface IRegionRepository
    {
        Task<List<RegionDto>> GetAllAsync();
        Task<RegionDto?> GetByIdAsync(Guid Id);
        Task<RegionDto> CreateAsync(RegionDto region);
        Task<RegionDto?> UpdateAsync(Guid Id, Region region);
        Task<RegionDto> DeleteAsync(Guid Id);
        Task SaveAsync();



    }
}
