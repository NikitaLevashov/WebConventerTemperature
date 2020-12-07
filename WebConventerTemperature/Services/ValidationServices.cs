using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConventerTemperature.Services
{
    public class ValidationServices : IValidationServices
    {
        const double _min = -273.15;
        public bool AbsolutabsoluteMinimum (double value)
        {
            if( value < _min)
            {
                return false;
            }

            return true;
        }
    }
}
