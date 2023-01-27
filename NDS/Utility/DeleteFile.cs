using NDS.Models.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace NDS.Utility
{
    public class DeleteFile : IDeleteFile
    {

        private readonly IWebHostEnvironment _hosting;

        public DeleteFile(IWebHostEnvironment hosting)
        {
            _hosting = hosting;
        }



        public void DeleteFileFromHost(string filename, string normalpath, string thumbpath = "")
        {

            try
            {

                if (string.IsNullOrEmpty(filename))
                {
                    return;
                }



                // normal 
                if (!string.IsNullOrEmpty(normalpath))
                {
                    var pathnormal = Path.Combine(_hosting.WebRootPath , normalpath , filename);

                    if (File.Exists(pathnormal))
                    {
                        File.Delete(pathnormal);
                    }
                }



                // thumnail  
                if (!string.IsNullOrEmpty(thumbpath))
                {
                    var pathtumbnail = Path.Combine(_hosting.WebRootPath, thumbpath , filename);

                    if (File.Exists(pathtumbnail))
                    {
                        File.Delete(pathtumbnail);
                    }
                }




            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
