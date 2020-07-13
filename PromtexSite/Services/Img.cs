using FreeImageAPI;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Services
{
    public static class Img
    {
        public static async Task<string> AddFile(IFormFile file, string rootPath, string AddPath)
        {
            if (file != null)
            {
                if (file.ContentType == "image/jpg" || file.ContentType == "image/jpeg")
                {
                    string fileName = Guid.NewGuid() + ".jpg";
                    string pathToSend = "/"+ AddPath + $"/{fileName}";
                    string path = Path.Combine(rootPath, AddPath, fileName);
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        var image = FreeImage.LoadFromStream(memoryStream, FREE_IMAGE_LOAD_FLAGS.JPEG_ACCURATE);
                        FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, image, path, FREE_IMAGE_SAVE_FLAGS.JPEG_OPTIMIZE);
                    }

                    return pathToSend;
                }
                else if (file.ContentType == "image/png")
                {
                    string fileName = Guid.NewGuid() + ".png";
                    string pathToSend = "/"+ AddPath + $"/{fileName}";
                    string path = Path.Combine(rootPath, AddPath, fileName);
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var image = FreeImage.LoadFromStream(memoryStream, FREE_IMAGE_LOAD_FLAGS.PNG_IGNOREGAMMA);
                        FreeImage.Save(FREE_IMAGE_FORMAT.FIF_PNG, image, path, FREE_IMAGE_SAVE_FLAGS.PNG_Z_BEST_COMPRESSION);
                    }
                    return pathToSend;
                }

            }
            return null;
        }
    }
}
