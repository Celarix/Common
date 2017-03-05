using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Validation
{
    /// <summary>
    /// Contains static methods with additional validation logic.
    /// </summary>
    public static class Validators
    {
        public static bool IsValid<TEnum>(this TEnum enumValue)
        where TEnum : struct
        {
            // From http://stackoverflow.com/a/23177585/2709212
            var firstChar = enumValue.ToString()[0];
            return (firstChar < '0' || firstChar > '9') && firstChar != '-';
        }
    }
}
