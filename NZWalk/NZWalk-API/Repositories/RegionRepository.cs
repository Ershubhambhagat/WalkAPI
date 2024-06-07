#region Using
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<RegionDto> CreateAsync(AddRegionRequestDTO addRegionRequestDTO)
        {
           
                // DTO To Domain
                var RegionDomainModel = new RegionDto
                {
                    Code = addRegionRequestDTO.Code,
                    Name=addRegionRequestDTO.Name,
                    RegionImageUrl=addRegionRequestDTO.RegionImageUrl,
                };

                // use RegionDomainModel to Create 
                await _nZWalksDBContext.Regions.AddAsync(RegionDomainModel);
                await SaveAsync();
                //Map Domain Model back to DTO
                var regionDTO = new RegionDto
                { 
                    Id=RegionDomainModel.Id,
                    Name=RegionDomainModel.Name,
                    RegionImageUrl=RegionDomainModel.RegionImageUrl,
                    Code=addRegionRequestDTO.Code,

                };
               return regionDTO;
            
           
        }

        #endregion

        #region DeleteAsync

        public async Task<RegionDto> DeleteAsync(Guid Id)
        {
            var ExistingRegion = await GetByIdAsync(Id);
            
            if(ExistingRegion is null)
            {
                return null;
            }
             _nZWalksDBContext.Regions.Remove(ExistingRegion);
            SaveAsync();
            return ExistingRegion;
        }


        #endregion

        #region GetAllAsync
        public async Task<List<RegionDto>> GetAllAsync()
        {

            return await _nZWalksDBContext.Regions.ToListAsync(); 
            
        }

        #endregion

        #region GetByIdAsync

        public async Task<RegionDto> GetByIdAsync(Guid Id)
        {
            return await _nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
          
        }
        #endregion

        #region SaveAsync
        public Task SaveAsync()
        {
            _nZWalksDBContext.SaveChangesAsync();
            return Task.CompletedTask;
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
            existingRegion.Name= region.Name;
            await SaveAsync();
            return existingRegion;


        }

        #endregion

    }
}
