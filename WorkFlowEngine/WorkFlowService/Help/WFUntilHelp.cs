/********************************************************************************
** Class Name:   WFUntilHelp 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WFUntilHelp class
*********************************************************************************/

using System;
using System.IO;
using CommonLibraSystem.LinqmmonLibrary.Help;

namespace WorkFlowService.Help
{
    public class WFUntilHelp
    {
        public static ActivityState GetActivityStateByName(string activityState)
        {
            ActivityState result;
            if (Enum.TryParse(activityState, true, out result))
                return result;
            return ActivityState.Read;
        }

        public static WorkFlowState GetWorkFlowStateByName(string workFlowState)
        {
            WorkFlowState result;
            if (Enum.TryParse(workFlowState, true, out result))
                return result;
            return WorkFlowState.Common;
        }

        public static ApplicationState GetApplicationState(string applicationState)
        {
            ApplicationState result;
            if (Enum.TryParse(applicationState, true, out result))
                return result;
            return ApplicationState.Draft;
        }

        private static string RunPath
        {
 /// <summary>
        /// Get enum type by data base type 
        /// </summary>
        /// <param name="datenumType">Enum type</param>
        /// <returns>Enum type </returns>
        public static T GetEnumTypeByDataBaseType<T>(string enum where T:  struct 
        {
            return (from sqlSourceType in Enum.GetNames(typeof(T))
                    where string.Compare(sqlSourceType, dataBaseenumringComparison.OrdinalIgnoreCase) == 0
                    select (T)Enum.Parse(typeof(T), sqlSourceType)).FirstOrDefault()ring RunPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static string SqliteFilePath
        {
            get { return Path.Combine(RunPath, WFConstants.SqliteFileNameTags); }
        }
    }
}
