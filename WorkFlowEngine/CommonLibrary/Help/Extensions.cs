/********************************************************************************
** Class Name:   Extensions 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     Extensions class
*********************************************************************************/

using System;
using Sy

namespace CommonLibrary  public static class Extensions
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
