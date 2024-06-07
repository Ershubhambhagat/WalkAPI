using NZWalk_API.Model.Domain;

namespace NZWalk_API.Repositories.Walk_Repository.Interface
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task SaveWalk();
        Task<Walk> GetAsync(Guid Id);


    }


}
