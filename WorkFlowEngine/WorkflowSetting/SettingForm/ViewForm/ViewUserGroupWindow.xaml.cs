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
using WorkFlowService.IDAL;
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
        public ViewUserGroupWindow(IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            UserOperationDAL = userOperationDAL;
            InitData();
            
        }

        private IUserOperationDAL UserOperationDAL { get; set; }

        private void InitData()
        {
            DgUserGroup.ItemsSource = UserOperationDAL.DataOperationInstance.QueryAll<UserGroupModel>();
            DgUserGroup.Items.Refresh();
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
            var editUserGroupWindow = new UserGroupRelationWindow(DgUserGroupSelectEntity, OperationAction.Modify, UserOperationDAL);
            editUserGroupWindow.ShowDialog();
        }

        private void RowDeleteClick(object sender, RoutedEventArgs e)
        {
            if (DgUserGroupSelectEntity == null) return;
            UserOperationDAL.DataOperationInstance.Remove<UserGroupModel>(DgUserGroupSelectEntity.Id);
            UserOperationDAL.DeleteUserGroupAllRoleRelation(DgUserGroupSelectEntity.Id);
            UserOperationDAL.DeleteUserGroupAllRoleRelation(DgUserGroupSelectEntity.Id);
            InitData();
        }

        private void RowAddNewClick(object sender, RoutedEventArgs e)
        {
            var addUserGroupWindow = new UserGroupRelationWindow(UserOperationDAL);
            addUserGroupWindow.Show();
        }

        private void RowRefreshClick(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}
