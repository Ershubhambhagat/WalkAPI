using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalk_API.Data;
using NZWalk_API.Model.Domain;
using NZWalk_API.Repositories.Walk_Repository.Interface;

namespace NZWalk_API.Repositories.Walk_Repository
{
    public class WalkRepository : IWalkRepository
    {

        #region MyRegion
        private readonly NZWalksDBContext _dbContext;

        public WalkRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }



        #endregion
        #region MyRegion
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await SaveWalk();
            return walk;
        }

        public Task<Walk> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }




        #endregion

        #region SaveWalk
        public async Task SaveWalk()
        {
            await _dbContext.SaveChangesAsync();
        }


        #endregion

        #region MyRegion



        #endregion
        #region MyRegion


        #endregion
        #region MyRegion\


        #endregion
    }
}
