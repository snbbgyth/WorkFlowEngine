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
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLibrary.Help
{
   public  class CommonUnitlHelp
    {
       /// <summary>
       /// Get enum type by data base type 
       /// </summary>
       /// <param name="enumName">Enum type</param>
       /// <returns>Enum type </returns>
       public static T GetEnumTypeByName<T>(string enumName) where T : struct
       {
           return (from enumType in Enum.GetNames(typeof(T))
                   where String.Compare(enumType, enumName, StringComparison.OrdinalIgnoreCase) == 0
                   select (T)Enum.Parse(typeof(T), enumType)).FirstOrDefault();
       }

       public static string LogPathTags
       {
           get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommonConstants.LogFolderNameTags); }
       }
    }
}
