using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConventerTemperature.Services
{
    public class ValidationServices : IValidationServices
    {
        const double minValidTemperature = -273.15;
        public bool IsValidTemperature (int value)
        {
            if( value < minValidTemperature)
            {
                return false;
            }

            return true;
        }
    }
}
