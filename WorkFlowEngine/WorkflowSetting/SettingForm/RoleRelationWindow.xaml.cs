using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorkflowSetting.SettingForm
{
    using WorkFlowService.BLL;
    using WorkFlowService.Model;

    /// <summary>
    /// RoleRelationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoleRelationWindow : Window
    {
        public RoleRelationWindow()
        {
            InitializeComponent();
        }

        private void InitData(RoleInfoModel entity)
        {
            TxtRoleId.Text = entity.ID;
            TxtRoleName.Text = entity.RoleName;
            TxtRoleDisplayName.Text = entity.RoleDisplayName;
            ExistUserGroupList = UserOperationBLL.Current.QueryAllUserGroupByRoleId(entity.ID);
            ExistUserInfoList = UserOperationBLL.Current.QueryAllUserInfoByRoleId(entity.ID);
            ExistActionInfoList = UserOperationBLL.Current.QueryAllActionInfoByRoleId(entity.ID);
            ClearBindData();
            LvUserGroupName.ItemsSource = ExistUserGroupList;
            LvUserName.ItemsSource = ExistUserInfoList;
            LvActionName.ItemsSource = ExistActionInfoList;
            LvWorkflowState.ItemsSource = UserOperationBLL.Current.QueryAllWorkflowStateByRoleId(entity.ID);
        }

        private void ClearBindData()
        {
            LvUserGroupName.Items.Clear();
            LvUserName.Items.Clear();
            LvActionName.Items.Clear();
            LvWorkflowState.Items.Clear();
        }

        private void ModifyUserGroupList()
        {
            var userGroupList = LvUserGroupName.ItemsSource as List<UserGroupModel>;
            if (userGroupList == null) return;
            var entityList = userGroupList.Where(entity => ExistUserGroupList.Any(t => t.ID != entity.ID));
            foreach (var entity in entityList)
            {
                UserOperationBLL.Current.AddUserGroupRole(entity.ID,TxtRoleId.Text);
            }
            var removeList = ExistUserGroupList.Where(entity => userGroupList.Any(t => t.ID != entity.ID));
            foreach (var entity in removeList)
            {
                UserOperationBLL.Current.DeleteUserGroupRole(entity.ID, TxtRoleId.Text);
            }
        }

        private void ModifyUserInfoList()
        {
            var userInfoList =LvUserName.ItemsSource as List<UserInfoModel>;
            if (userInfoList == null) return;
            var entityList = userInfoList.Where(entity =>ExistUserInfoList.Any(t => t.ID != entity.ID));
            foreach (var entity in entityList)
            {
                UserOperationBLL.Current.AddUserRole(entity.ID,TxtRoleId.Text);
            }
            var removeList = ExistUserInfoList.Where(entity => userInfoList.Any(t => t.ID != entity.ID));
            foreach (var entity in removeList)
            {
                UserOperationBLL.Current.DeleteUserRole( entity.ID,TxtRoleId.Text);
            }
        }

        private void ModifyActionListList()
        {
            var actionInfoList =LvActionName.ItemsSource as List<OperationActionInfoModel>;
            if (actionInfoList == null) return;
            var entityList = actionInfoList.Where(entity => ExistActionInfoList.Any(t => t.ID != entity.ID));
            foreach (var entity in entityList)
            {
                UserOperationBLL.Current.AddOperationActionInRole(entity.ID, TxtRoleId.Text);
            }
            var removeList = ExistActionInfoList.Where(entity => actionInfoList.Any(t => t.ID != entity.ID));
            foreach (var entity in removeList)
            {
                UserOperationBLL.Current.DeleteRoleOperationAction(TxtRoleId.Text,entity.ID);
            }
        }

        private List<UserInfoModel> ExistUserInfoList { get; set; }

        private List<UserGroupModel> ExistUserGroupList { get; set; }

        private List<OperationActionInfoModel> ExistActionInfoList { get; set; } 

        private void BtnAddUserGroupClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnModifyClick(object sender, RoutedEventArgs e)
        {
            ModifyUserGroupList();
            ModifyUserInfoList();
            ModifyActionListList();
        }

        private void BtnAddActionClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
