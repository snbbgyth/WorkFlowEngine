using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;
using WorkflowSetting.SettingForm.AddForm;
using WorkflowSetting.SettingForm.OperationForm;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// ViewUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewUserGroupWindow : Window
    {
        public ViewUserGroupWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            DgUserGroup.Items.Clear();
            DgUserGroup.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<UserGroupModel>();
            DgUserGroup.SelectionChanged += DgUserGroupSelectionChanged;
        }

        private void DgUserGroupSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (UserGroupModel item in DgUserGroup.SelectedItems)
            {
                DgUserGroupSelectEntity = item;
                break;
            }
        }

        private UserGroupModel DgUserGroupSelectEntity { get; set; }

        private void RowEditClick(object sender, RoutedEventArgs e)
        {
            if (DgUserGroupSelectEntity == null) return;
            var editUserGroupWindow = new UserGroupRelationWindow(DgUserGroupSelectEntity, OperationAction.Modify);
            editUserGroupWindow.ShowDialog();
        }

        private void RowDeleteClick(object sender, RoutedEventArgs e)
        {
            if (DgUserGroupSelectEntity == null) return;
            //UserOperationBLL.Current.DataOperationInstance.Remove<RoleInfoModel>(DgUserSelectEntity.Id);
        }

        private void RowAddNewClick(object sender, RoutedEventArgs e)
        {
            var addUserGroupWindow = new UserGroupRelationWindow();
            addUserGroupWindow.Show();
        }
    }
}
