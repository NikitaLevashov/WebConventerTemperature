using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebConventerTemperature.Models;
using WebConventerTemperature.Services;
using WebConventerTemperature.Util;

namespace WebConventerTemperature.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IValidationServices _validationServices;
        public HomeController(IValidationServices validationServices)
        {
            _validationServices = validationServices;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
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
                return new HtmlResult("<h2> Статус ошибки 400. Некорректное значение!</h2>");
            }

            return new HtmlResult($"<h2>Температура по Цельсию - {_celsiusValue}. Конвертируем в Фаренгейт: \n\n\nТемпература по Фаренгейту - {_fahrenheitValue}<h2>");
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
