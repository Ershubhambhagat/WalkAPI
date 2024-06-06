#region Using
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

        public Task<RegionDto> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region GetAllAsync
        public async Task<List<RegionDto>> GetAllAsync()
        {

            return await _nZWalksDBContext.Regions.ToListAsync(); 
            
        }

        #endregion

        #region GetById

        public Task<RegionDto> GetById(Guid Id)
        {
            throw new NotImplementedException();
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

        public Task<RegionDto> UpdateAsync(Guid Id, UpdateResionRequestDTO updateResionRequestDTO)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
