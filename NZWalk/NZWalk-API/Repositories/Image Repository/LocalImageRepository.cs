using NZWalk_API.Data;
using NZWalk_API.Model.Domain;

namespace NZWalk_API.Repositories.Image_Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor localHostPathAccessor;
        private readonly NZWalksDBContext _db;

        public LocalImageRepository(IWebHostEnvironment webHost,IHttpContextAccessor LocalHostPathAccessor,NZWalksDBContext nZWalksDB)
        {
            this.webHost = webHost;
            localHostPathAccessor = LocalHostPathAccessor;
            _db = nZWalksDB;
        }


        public async Task<Image> Uplode(Image image)
        {

                var localfilepath = Path.Combine(webHost.ContentRootPath, "ImageStore",
                    $"{image.FileName}{image.FileExtension}");


            //uplode image to locale path 
            using var stream = new FileStream(localfilepath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            

            //https://localhost:1234/images/image.jpg
            var urlFilePath = $"{localHostPathAccessor.HttpContext.Request.Scheme}" +
                $"://{localHostPathAccessor.HttpContext.Request.Host}" +
                $"{localHostPathAccessor.HttpContext.Request.PathBase}" +
                $"/ImageStore/{image.FileName}{image.FileExtension}";
            image.FilePath= urlFilePath;
            //Add to database

            await _db.Images.AddAsync(image);
            await _db.SaveChangesAsync();
            return image;
            

        }
    }
}
