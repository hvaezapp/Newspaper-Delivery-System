using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NDS.Models.Services
{
    public interface IUploadFile
    {
        string SaveImage(IEnumerable<IFormFile> files, string uploadPath, string uploadthumbnailPath);

        string SaveFile(IFormFile file, string uploadPath);
    }

}
