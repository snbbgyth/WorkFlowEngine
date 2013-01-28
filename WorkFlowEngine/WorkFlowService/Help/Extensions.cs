﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowService.Help
{
   public static class Extensions
    {
       public static bool CompareEqualIgnoreCase(this string valueA, string valueB)
       {
           return string.Compare(valueA, valueB, StringComparison.OrdinalIgnoreCase) == 0;
       }

       public static string ConvertSqliteDateTime(this DateTime? datetime)
       {
           if (datetime == null) return string.Empty;
           return ((DateTime)datetime).ToString("s");
       }
    }
}
