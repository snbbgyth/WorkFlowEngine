using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommonLibrary.IDAL;

namespace WorkflowSetting.Help
{
    public static class SettingHelp
    {
        public static void RemoveItemByCondition<T>(ListView lv, List<string> idList) where T : ITableModel
        {
            var userRoleSource = lv.ItemsSource as List<T>;
            if (userRoleSource != null)
            {
                userRoleSource.RemoveAll(entity => idList.Contains(entity.ID));
                lv.Items.Clear();
                lv.ItemsSource = userRoleSource;
            }
        }

        public static void MoidfyListByCondition<T>(ListView lv, Func<string, string, bool> addAction,
                                       Func<string, string, bool> removeAction, IEnumerable<T> existList, string leftItemId = null, string rightItemId = null) where T : ITableModel
        {
            var entityList = lv.ItemsSource as List<T>;
            if (entityList == null) return;
            var addList = entityList.Where(entity => existList.Any(t => t.ID != entity.ID));
            foreach (var entity in addList)
            {
                if (!string.IsNullOrEmpty(leftItemId))
                    addAction(leftItemId, entity.ID);
                else if (!string.IsNullOrEmpty(rightItemId))
                    addAction(entity.ID, rightItemId);
            }
            var removeList = existList.Where(entity => entityList.Any(t => t.ID != entity.ID));
            foreach (var entity in removeList)
            {
                if (!string.IsNullOrEmpty(leftItemId))
                    removeAction(leftItemId, entity.ID);
                else if (!string.IsNullOrEmpty(rightItemId))
                    removeAction(entity.ID, rightItemId);
            }
        }

        public static void AddRelationByCondition<T>(ListView lv,Func<string,string,bool> addAction, string itemId)where T:ITableModel
        {
            var entityList = lv.ItemsSource as List<T>;
            if (entityList == null || !entityList.Any()) return;
            entityList.ForEach(entity => addAction(itemId, entity.ID));
        }
    }
}
