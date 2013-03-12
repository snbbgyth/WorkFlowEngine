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
    using Help;

    /// <summary>
    /// RoleRelationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoleRelationWindow : Window
    {
        public RoleRelationWindow()
        {
            InitializeComponent();
        }

        public RoleRelationWindow(RoleInfoModel entity) : this()
        {
            InitData(entity);
        }

        public RoleRelationWindow(string roleId) : this()
        {
            var entity = DataOperationBLL.Current.QueryByID<RoleInfoModel>(roleId);
            InitData(entity);
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
            SettingHelp.MoidfyListByCondition(LvUserGroupName, UserOperationBLL.Current.AddUserGroupRole, UserOperationBLL.Current.DeleteUserGroupRole, ExistUserGroupList,null,TxtRoleId.Text);
        }

        private void ModifyUserInfoList()
        {
            SettingHelp.MoidfyListByCondition(LvUserName, UserOperationBLL.Current.AddUserRole, UserOperationBLL.Current.DeleteUserRole, ExistUserInfoList, null, TxtRoleId.Text);
        }

        private void ModifyActionListList()
        {
            SettingHelp.MoidfyListByCondition(LvActionName, UserOperationBLL.Current.AddOperationActionInRole, UserOperationBLL.Current.DeleteOperationActionInRole, ExistActionInfoList, null, TxtRoleId.Text);
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

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRemoveUserGroupClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserGroupModel>(LvUserGroupName,new List<string>());
        }

        private void BtnRemoveUserRoleClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserInfoModel>(LvUserName, new List<string>());
        }

        private void BtnRemoveActionClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<OperationActionInfoModel>(LvActionName,new List<string>());
        }
    }
}
