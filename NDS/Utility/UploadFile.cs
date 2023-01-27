
using NDS.Models.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace NDS.Utility
{
    public class UploadFile : IUploadFile
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILogger _logger;
        public UploadFile(IWebHostEnvironment appEnvironment , ILogger logger)
        {
            _appEnvironment = appEnvironment;
            _logger = logger;
        }




        public string  SaveImage(IEnumerable<IFormFile> files, string uploadPath, string uploadthumbnailPath)
        {
            string filename = "";
            try
            {

                var upload = Path.Combine(_appEnvironment.WebRootPath, uploadPath);
                var thumb = Path.Combine(_appEnvironment.WebRootPath, uploadthumbnailPath);

                
                foreach (var file in files)
                {
                    filename = AppUtility.GenerateGuidToken() + Path.GetExtension(file.FileName);

                    using (var fs = new FileStream(Path.Combine(upload, filename), FileMode.Create))
                    {
                        file.CopyTo(fs);


                    }
                }
              
                if (uploadthumbnailPath != "")
                {
                    ImageResizer img = new ImageResizer();
                    img.Resize(upload + filename, thumb + filename);


                }
                
            }
            catch (Exception ex)
            {

                _logger.Log(ex.Message, "UploadFile,SaveFile");
            }

            return filename;
        }

        public string SaveFile(IFormFile file, string uploadPath)
        {

            string filename = "";
            try
            {
                // base path
                var upload = Path.Combine(_appEnvironment.WebRootPath, uploadPath);

                filename = AppUtility.GenerateGuidToken() + Path.GetExtension(file.FileName);

                // save file to base path
                using (var fs = new FileStream(Path.Combine(upload, filename), FileMode.Create))
                {
                    file.CopyTo(fs);

                }

            }
            catch (Exception ex)
            {

                _logger.Log(ex.Message, "UploadFile,SaveFile");
            }

            return  filename;
        }
    }
}
