using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniatureIOC.Helpers
{
    public class RegexHelp
    {
        public static bool IsMatch(string regexStr, string inputStr)
        {
            var regex = new Regex(regexStr);
            Match match = regex.Match(inputStr);

            return match.Success;
        }
    }
}
