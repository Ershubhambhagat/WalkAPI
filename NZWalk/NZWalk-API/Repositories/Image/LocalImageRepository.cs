using NZWalk_API.Data;

namespace NZWalk_API.Repositories.Image
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalksDBContext _nZWalksDBContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDBContext nZWalksDBContext)
        {
            _webHostEnvironment = webHostEnvironment;
            //Create Url Path
            _httpContextAccessor = httpContextAccessor;
            _nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<Model.Domain.Image> Upload(Model.Domain.Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");
                


            //uplode Image To Local Path 
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // http://localhost:5577/Images/image.png
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/" +
                $"{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Image To the image Table 
            await _nZWalksDBContext.Images.AddAsync(image);
            await _nZWalksDBContext.SaveChangesAsync();
            return image;
        }

    }
}
