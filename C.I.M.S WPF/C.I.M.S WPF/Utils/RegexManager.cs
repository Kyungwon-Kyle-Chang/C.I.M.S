using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils
{
    public class RegexManager
    {
        private static readonly Regex _uDoubleRegex = new Regex("[^0-9.]"); //regex that matches disallowed text
        private static readonly Regex _nullRegex = new Regex("\0");

        public static bool IsTextUnsignedDouble(string text) { return !_uDoubleRegex.IsMatch(text); }
        public static bool IsNotNull(string text) { return !_nullRegex.IsMatch(text); }
        public static bool IsNotBlank(string text) { return !text.Equals(""); }
    }
}
