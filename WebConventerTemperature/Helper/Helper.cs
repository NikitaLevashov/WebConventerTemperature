using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConventerTemperature.Models;
using WebConventerTemperature.Services;

namespace WebConventerTemperature.Helper
{
    public class Helper 
    {
        
        static double _fahrenheitValue;
        static public double ConvertTemperature(int valueTemperature)
        {
            try
            {
                return _fahrenheitValue = (valueTemperature * 9 / 5) + 32;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");

                return 0;
            }

        }

    }
}
