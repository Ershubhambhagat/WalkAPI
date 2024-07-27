using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO.ImageDTOs;

namespace NZWalk_API.Repositories.Image_Repository
{
    public interface IImageRepository
    {
        public Task<Image> Uplode(Image image);
    }
}
