using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk_API.Data;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;
using NZWalk_API.Repositories.Walk_Repository.Interface;
using System.Net.WebSockets;

namespace NZWalk_API.Repositories.Walk_Repository
{
    public class WalkRepository : IWalkRepository
    {

        #region Ctor
        private readonly NZWalksDBContext _dbContext;

        public WalkRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }



        #endregion

        #region CreateWalkAsync
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await SaveWalk();
            return walk;
        }
  
        #endregion

        #region SaveWalk
        public async Task SaveWalk()
        {
            await _dbContext.SaveChangesAsync();
        }


        #endregion

        #region GetWalkById

        public async Task<Walk?> GetWalkById(Guid Id)
        {
            return await _dbContext.Walks.Include("Diffucalty").Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == Id);
        }



        #endregion

        #region GetAllWalkAsync or Filtering
        public async Task<List<Walk>> GetAllWalkAsync(string? FilterOn = null, string? FilteQuary = null)
        {
            var Walks = _dbContext.Walks.Include("Diffucalty").Include(x => x.Region).AsQueryable();
            if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilteQuary) == false)
            {
                if (FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks=Walks.Where(x=>x.Name.Contains(FilteQuary));
                }
                
                
            }

            return await Walks.ToListAsync();
        }
        #endregion

        #region UpdateWAlkAsync
       
        public async Task<Walk?> UpdateWAlkAsync(Guid Id,Walk walk)
        {
            var existingWalk =await _dbContext.Walks.FirstOrDefaultAsync(x=>x.Id==Id);  
            if (existingWalk == null) 
            { return null;  }
            existingWalk.Id = Id;
            existingWalk.Name = walk.Name;
            //existingWalk.Region = walk.Region;
            existingWalk.Description= walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.Diffucalty = walk.Diffucalty;
            existingWalk.RegionId=walk.RegionId;
            await SaveWalk();
            return walk;

        }
        #endregion

        #region DeleteAsync
        public async Task<Walk?> DeleteAsync(Guid Id)
        {
            var data =await GetWalkById(Id);
            if(data== null)
            {
                return null;
            }
            _dbContext.Walks.Remove(data);
            await SaveWalk();
            return data;
        }

        #endregion
        
    }
}
