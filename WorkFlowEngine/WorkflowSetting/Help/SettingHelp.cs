using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
                userRoleSource.RemoveAll(entity => idList.Contains(entity.Id));
                lv.Items.Clear();
                lv.ItemsSource = userRoleSource;
            }
        }

        public static void RemoveItemByCondition<T>(ListView lv ) where T : ITableModel
        {
            var entityList = lv.ItemsSource as List<T>;
            foreach (T item in lv.SelectedItems)
            {
                if (entityList != null) entityList.Remove(item);
            }
            lv.ItemsSource = entityList;
            lv.Items.Refresh();
        }


        public static void MoidfyListByCondition<T>(ListView lv, Func<string, string, bool> addAction,
                                       Func<string, string, bool> removeAction, IEnumerable<T> existList, string leftItemId = null, string rightItemId = null) where T : ITableModel
        {
            var entityList = lv.ItemsSource as List<T>;
            if (entityList == null) return;
            var addList = entityList.Where(entity =>existList==null|| existList.All(t => t.Id != entity.Id));
            foreach (var entity in addList)
            {
                if (!string.IsNullOrEmpty(leftItemId))
                    addAction(leftItemId, entity.Id);
                else if (!string.IsNullOrEmpty(rightItemId))
                    addAction(entity.Id, rightItemId);
            }
            if (existList != null)
            {
                var removeList = existList.Where(entity => !entityList.Exists(t => t.Id == entity.Id));
                foreach (var entity in removeList)
                {
                    if (!string.IsNullOrEmpty(leftItemId))
                        removeAction(leftItemId, entity.Id);
                    else if (!string.IsNullOrEmpty(rightItemId))
                        removeAction(entity.Id, rightItemId);
                }
            }
            existList = entityList;
        }

        public static void AddRelationByCondition<T>(ListView lv,Func<string,string,bool> addAction, string itemId)where T:ITableModel
        {
            var entityList = lv.ItemsSource as List<T>;
            if (entityList == null || !entityList.Any()) return;
            entityList.ForEach(entity => addAction(itemId, entity.Id));
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static List<T> DeepCopy<T>(this List<T> entityList)
        {
            if (entityList == null || entityList.Count == 0) return new List<T>();
            using (var memoryStream = new MemoryStream())
            {
               IFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, entityList);
                memoryStream.Position = 0;
                return binaryFormatter.Deserialize(memoryStream) as List<T>;
 
            }
        }

        public static void AddEntityRange<T>(ListView lv, List<T> selectList) where T : ITableModel
        {
            if (selectList == null) return;
            List<T> entityList;
            entityList = lv.ItemsSource as List<T>;
            if (entityList == null) entityList = new List<T>();
            entityList.AddRange(selectList.FindAll(entity => !entityList.Exists(item => item.Id == entity.Id)));


            lv.ItemsSource = entityList;
            lv.Items.Refresh();
        }
    }
}
