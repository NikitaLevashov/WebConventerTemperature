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
            
            if(conv.Conventer== "conventer")
            {
               if(_validationServices.AbsolutabsoluteMinimum(conv.СelsiusValue))
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

        public HtmlResult ActionResultIndexHtml(double _celsiusValue)
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

        public FileResult GetStream(double _celsiusValue)
        {

            string fileType = "application/txt";
            string fileName = "file.txt";
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/file.TXT");

            double _fahrenheitValue = (_celsiusValue * 9 / 5) + 32;

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes($"{_celsiusValue} C°  =>  {_fahrenheitValue} F° ");
               
                fstream.Write(input, 0, input.Length);
            }
            
            FileStream fs = new FileStream(path, FileMode.Open);

            return File(fs, fileType, fileName);

        }

        public FileResult GetBytes(double _celsiusValue)
        {
            string fileType = "application/txt";
            string fileName = "file.txt";
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Files/TXT");

            double _fahrenheitValue = (_celsiusValue * 9 / 5) + 32;

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes($"{_celsiusValue} C°  =>  {_fahrenheitValue} F° ");

                fstream.Write(input, 0, input.Length);
            }

            byte[] mas = System.IO.File.ReadAllBytes(path);
            
            return File(mas, fileType, fileName);
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
