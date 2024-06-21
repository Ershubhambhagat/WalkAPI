#region Using
using Microsoft.EntityFrameworkCore;
using NZWalk_API.Data;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
using NZWalk_API.Repositories.Interface;
#endregion
namespace NZWalk_API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        #region CTOR
        private readonly NZWalksDBContext _nZWalksDBContext;

        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            _nZWalksDBContext = nZWalksDBContext;
        }


        #endregion

        #region CreateAsync
        public async Task<RegionDto> CreateAsync(RegionDto region)
        {

            var regionDto = await _nZWalksDBContext.Regions.AddAsync(region);
            await SaveAsync();
            //Map Domain Model back to DTO


            return region;
        }

        #endregion

        #region DeleteAsync

        public async Task<RegionDto> DeleteAsync(Guid Id)
        {
            var ExistingRegion = await GetByIdAsync(Id);

            if (ExistingRegion is null)
            {
                return null;
            }
            _nZWalksDBContext.Regions.Remove(ExistingRegion);
            await SaveAsync();
            return ExistingRegion;
        }


        #endregion

        #region GetAllAsync
        public async Task<List<RegionDto>> GetAllAsync()
        {

            var region = await _nZWalksDBContext.Regions.ToListAsync();
            return region;


        }

        #endregion

        #region GetByIdAsync

        public async Task<RegionDto> GetByIdAsync(Guid Id)
        {
            return await _nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

        }
        #endregion

        #region SaveAsync
        public async Task SaveAsync()
        {
            await _nZWalksDBContext.SaveChangesAsync();

        }

        #endregion

        #region UpdateAsync

        public async Task<RegionDto?> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await _nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            existingRegion.Name = region.Name;
            await SaveAsync();
            return existingRegion;


        }

        #endregion

    }
}
