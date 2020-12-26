using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebConventerTemperature.Services
{
    public class TextServices
    {
        static public  IWebHostEnvironment _appEnvironment;
        public TextServices(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public const string fileZipType = "application/zip";
        public const string fileType = "application/txt";
        public const string fileName = "text.txt";
        public const string fileZipName = "text.zip";
        public static string path = Path.Combine(_appEnvironment.ContentRootPath, $"Files/{fileName}");
        public static string pathZip = Path.Combine(_appEnvironment.ContentRootPath, $"Files/{fileZipName}");

    }
}
