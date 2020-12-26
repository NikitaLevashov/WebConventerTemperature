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
using System.IO.Compression;


namespace WebConventerTemperature.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValidationServices _validationServices;

        double _fahrenheitValue;
        public HomeController(IValidationServices validationServices)
        {
            _validationServices = validationServices ?? throw new ArgumentNullException();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TemperatureConverter(TemperatureModel modelTemperature)
        {
            if (_validationServices.IsValidTemperature(modelTemperature.Value))
            {
                _fahrenheitValue = Helper.Helper.ConvertTemperature(modelTemperature.Value);
            }
            else
            {
                return BadRequest();
            }

            ViewData["СelsiusValue"] = modelTemperature.Value;
            ViewData["FahrenheitValue"] = _fahrenheitValue;

            return View();
              
        }

        [HttpGet]
        public IActionResult About()
        {
            return Redirect("http://it-academy.by");
        }
        public HtmlResult ConverterTemperatureReturnHtmlResult(TemperatureModel modelTemperature)
        {
            //~Home/ActionResultIndexHtml?_celsiusValue=12.3
            
            if (_validationServices.IsValidTemperature(modelTemperature.Value))
            {
                _fahrenheitValue = Helper.Helper.ConvertTemperature(modelTemperature.Value);
            }
            else
            {
                return new HtmlResult("<h2> Статус ошибки 400. Некорректное значение!</h2>");
            }

            return new HtmlResult($"<h2>Температура по Цельсию - {modelTemperature.Value}. Конвертируем в Фаренгейт: \n\n\nТемпература по Фаренгейту - {_fahrenheitValue}<h2>");
        }
        public IActionResult GetFile(TemperatureModel modelTemperature, FileType file)
        {
            if (_validationServices.IsValidTemperature(modelTemperature.Value))
            {
                _fahrenheitValue = Helper.Helper.ConvertTemperature(modelTemperature.Value);
            }
            else
            {
                return new HtmlResult("<h2> Статус ошибки 400. Некорректное значение!</h2>");
            }

            using (FileStream fstream = new FileStream(TextServices.path, FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes($"{modelTemperature.Value} C°  =>  {_fahrenheitValue} F° ");

                fstream.Write(input, 0, input.Length);
            }

            switch (file)
            {
                case FileType.txt:
                    FileStream fileStream = new FileStream(TextServices.path, FileMode.Open);
                    return File(fileStream, TextServices.fileType, TextServices.fileName);

                case FileType.streamOfBytes:
                    byte[] mas = System.IO.File.ReadAllBytes(TextServices.path);
                    return File(mas, TextServices.fileType, TextServices.fileName);

                case FileType.zip:
                    using (FileStream sourceStream = new FileStream(TextServices.path, FileMode.Open))
                    {
                        using (FileStream targetStream = new FileStream(TextServices.pathZip, FileMode.OpenOrCreate))
                        {
                            using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(compressionStream);

                            }
                        }
                    }

                    FileStream fileStreamZip = new FileStream(TextServices.pathZip, FileMode.Open);
                    return File(fileStreamZip, TextServices.fileZipType, TextServices.fileZipName);

                default:
                    return BadRequest();
            }
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
