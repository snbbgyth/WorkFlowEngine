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
using CommonLibrary.IDAL;

namespace WorkflowSetting.SettingForm
{
    using WorkFlowService.BLL;
    using WorkFlowService.Model;
    using Help;

    /// <summary>
    /// UserRelationUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserRelationWindow : Window
    {
        public UserRelationWindow()
        {
            InitializeComponent();
        }

        public UserRelationWindow(string userId)
            : this()
        {
            var userInfoEntity = DataOperationBLL.Current.QueryByID<UserInfoModel>(userId);
            InitData(userInfoEntity);
        }

        public UserRelationWindow(UserInfoModel entity) : this()
        {
            InitData(entity);
        }

        private void InitData(UserInfoModel entity)
        {
            TxtUserId.Text = entity.ID;
            TxtUserName.Text = entity.UserName;
            TxtUserDisplayName.Text = entity.UserDisplayName;
            ClearDataBinding();
            ExistUserGroupList = UserOperationBLL.Current.QueryAllUserGroupByUserId(entity.ID);
            LvUserGroupName.ItemsSource = ExistUserGroupList;
            ExistRoleInfoList = UserOperationBLL.Current.QueryAllUserRoleByUserId(entity.ID);
            LvUserRole.ItemsSource = ExistRoleInfoList;
        }

        private void ClearDataBinding()
        {
            LvUserGroupName.Items.Clear();
            LvUserRole.Items.Clear();
        }

        private List<RoleInfoModel> ExistRoleInfoList { get; set; }
        private List<UserGroupModel> ExistUserGroupList { get; set; }

        private void BtnModifyClick(object sender, RoutedEventArgs e)
        {
            ModifyUserGroupList();
            ModifyUserRoleList();
        }

        private void ModifyUserGroupList()
        {
            SettingHelp.MoidfyListByCondition(LvUserGroupName, UserOperationBLL.Current.AddUserInUserGroup, UserOperationBLL.Current.DeleteUserInUserGroup, ExistUserGroupList, TxtUserId.Text);
        }

        private void ModifyUserRoleList()
        {
            SettingHelp.MoidfyListByCondition(LvUserRole, UserOperationBLL.Current.AddUserRole, UserOperationBLL.Current.DeleteUserRole, ExistRoleInfoList,TxtUserId.Text);
        }

        private void BtnAddUserGroupClick(object sender, RoutedEventArgs e)
        {
            //TODO: Add user to user group from SelectUserGroupWindow
        }

        private void BtnAddRoleClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Todo: need test when LvUserGroupName.ItemsSource modify.the ExistUserGroupList is modify or not.
        private void BtnRemoveUserGroupClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserGroupModel>(LvUserGroupName, new List<string>());
        }

        private void BtnRemoveRoleClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<RoleInfoModel>(LvUserRole,new List<string>());
        }

 
    }
}
