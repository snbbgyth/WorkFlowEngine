/********************************************************************************
** Class Name:   Extensions 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     Extensions class
*********************************************************************************/

using System;

namespace CommonLibrary.Help
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
