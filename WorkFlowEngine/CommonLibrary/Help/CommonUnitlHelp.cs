/********************************************************************************
** Class Name:   UnitlHelp 
** Author：      spring yang
** Create date： 2013-2-16
** Modify：      spring yang
** Modify Date： 2013-2-16
** Summary：     UnitlHelp class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Help
{
   public  class CommonUnitlHelp
    {
       /// <summary>
       /// Get enum type by data base type 
       /// </summary>
       /// <param name="enumType">Enum type</param>
       /// <returns>Enum type </returns>
       public static T GetEnumTypeByDataBaseType<T>(string enumType) where T : struct
       {
           return (from sqlSourceType in Enum.GetNames(typeof(T))
                   where String.Compare(sqlSourceType, enumType, StringComparison.OrdinalIgnoreCase) == 0
                   select (T)Enum.Parse(typeof(T), sqlSourceType)).FirstOrDefault();
       }
    }
}
