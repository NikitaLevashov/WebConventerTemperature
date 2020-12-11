using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebConventerTemperature.Models;
using WebConventerTemperature.Services;
using WebConventerTemperature.Util;
using WebConventerTemperature.Helper;
using static WebConventerTemperature.Helper.Helper;
using System.IO.Compression;


namespace WebConventerTemperature.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValidationServices _validationServices;

        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(IValidationServices validationServices, IWebHostEnvironment appEnvironment)
        {
            _validationServices = validationServices;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ConventerTemperature conv)
        {

            if (conv.Conventer == "conventer")
            {
                if (_validationServices.AbsolutabsoluteMinimum(conv.СelsiusValue))
                {
                    conv.FahrenheitValue = (conv.СelsiusValue * 9 / 5) + 32;
                }
                else
                {
                    return BadRequest();
                }

            }

            ViewData["FahrenheitValue"] = conv.FahrenheitValue;

            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return Redirect("http://it-academy.by");
        }

        public HtmlResult ActionResultIndexHtml(int _celsiusValue)
        {
            //~Home/ActionResultIndexHtml?_celsiusValue=12.3
            double _fahrenheitValue;

            if (_validationServices.AbsolutabsoluteMinimum(_celsiusValue))
            {
                _fahrenheitValue = (_celsiusValue * 9 / 5) + 32;
            }
            else
            {
                return new HtmlResult("<h2> Статус ошибки 404. Некорректное значение!</h2>");
            }

            return new HtmlResult($"<h2>Температура по Цельсию - {_celsiusValue}. Конвертируем в Фаренгейт: \n\n\nТемпература по Фаренгейту - {_fahrenheitValue}<h2>");
        }


        public ActionResult GetFiles(int _celsiusValue, FileType file)
        {
            string fileZipType = "application/zip";
            string fileType = "application/txt";
            string fileName = "text.txt";
            string fileZipName = "text.zip";
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/text.txt");
            string pathZip = Path.Combine(_appEnvironment.ContentRootPath, "Files/text.zip");

            double _fahrenheitValue = (_celsiusValue * 9 / 5) + 32;

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes($"{_celsiusValue} C°  =>  {_fahrenheitValue} F° ");

                fstream.Write(input, 0, input.Length);
            }

            switch (file)
            {
                case FileType.txt:
                    FileStream fileStream = new FileStream(path, FileMode.Open);
                    return File(fileStream, fileType, fileName);

                case FileType.streamOfBytes:
                    byte[] mas = System.IO.File.ReadAllBytes(path);
                    return File(mas, fileType, fileName);

                case FileType.zip:

                    using (FileStream sourceStream = new FileStream(path, FileMode.Open))
                    {
                        using (FileStream targetStream = new FileStream(pathZip, FileMode.OpenOrCreate))
                        {
                            using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(compressionStream); 

                            }
                        }
                    }

                    FileStream fileStreamZip = new FileStream(pathZip, FileMode.Open);
                    return File(fileStreamZip, fileZipType, fileZipName);

            }

            return BadRequest();

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    
      
    }
    

}
