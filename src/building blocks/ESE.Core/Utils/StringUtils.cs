﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESE.Core.Utils
{
    public static class StringUtils
    {
        public static string JustNumbers(this string str, string input) 
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

    }
}