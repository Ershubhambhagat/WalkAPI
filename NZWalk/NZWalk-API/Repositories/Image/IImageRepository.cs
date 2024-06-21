namespace NZWalk_API.Repositories.Image
{
    public interface IImageRepository
    {
        public Task<NZWalk_API.Model.Domain.Image> Upload(NZWalk_API.Model.Domain.Image image);
    }
}
