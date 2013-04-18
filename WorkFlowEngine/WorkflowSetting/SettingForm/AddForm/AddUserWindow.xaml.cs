using System;
using System.Collections.Generic;
using System.Windows;
using WorkflowSetting.Help;
using WorkFlowService.Model;
using WorkFlowService.BLL;
using WorkflowSetting.SettingForm.SelectForm;

namespace WorkflowSetting.SettingForm.AddForm
{
    /// <summary>
    /// AddUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void BtnAddUserGroupClick(object sender, RoutedEventArgs e)
        {
            var selectUserGroupWindow = new SelectUserGroupWindow();
            if (selectUserGroupWindow.ShowDialog() == false)
            {
                SettingHelp.AddEntityRange(LvUserGroupName, selectUserGroupWindow.SelectUserGroupList);
            }
        }

        private void BtnRemoveUserGroupClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserGroupModel>(LvUserGroupName);
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (!CheckInput()) return;
            if (Add())
            {
                AddReportToUser();
            }
            LblMessage.Content = "Create successful!";
            BtnAdd.IsEnabled = false;
        }

        private void AddReportToUser()
        {
            if (!string.IsNullOrEmpty(ReportToId))
                UserOperationBLL.Current.AddUserReportToUser(Id, ReportToId);
        }

        private bool Add()
        {
            var entity = GetEntity();
            var result = UserOperationBLL.Current.DataOperationInstance.Insert(entity);
            if (result > 0)
            {
                Id = entity.Id;
                AddRelationList();
                return true;
            }
            return false;
        }

        private void AddRelationList()
        {
            SettingHelp.AddRelationByCondition<UserGroupModel>(LvUserGroupName, UserOperationBLL.Current.AddUserInUserGroup, Id);
            SettingHelp.AddRelationByCondition<RoleInfoModel>(LvUserRole, UserOperationBLL.Current.AddUserRole, Id);
        }

        private string ReportToId { get; set; }

        private string Id { get; set; }

        private bool CheckInput()
        {
            bool result = true;
            if (String.Compare(PbPassword.Password, PbConfimPassword.Password, StringComparison.Ordinal) != 0)
            {
                LblConfimPasswordError.Content = "Please input same password!";
                result = false;
            }
            return result;

        }

        private UserInfoModel GetEntity()
        {
            return new UserInfoModel
            {
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                UserName = TxtUserName.Text,
                Password = PbPassword.Password,
                UserDisplayName = TxtUserDisplayName.Text
            };
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAddUserRoleClick(object sender, RoutedEventArgs e)
        {
            var selectRoleWindow = new SelectRoleWindow();
            if (selectRoleWindow.ShowDialog() == false)
            {
                SettingHelp.AddEntityRange(LvUserRole, selectRoleWindow.SelectRoleInfoList);
            }
        }

        private void BtnRemoveUserRoleClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<RoleInfoModel>(LvUserRole);
        }

        private void BtnAddReportToUserClick(object sender, RoutedEventArgs e)
        {
            var selectUserWindow = new SelectUserWindow(1);
            if (selectUserWindow.ShowDialog() == false)
            {
                var entityList = selectUserWindow.SelectUserInfoList;
                if (entityList == null || entityList.Count == 0) return;
                var entity = entityList[0];
                ReportToId = entity.Id;
                TxtReportToUserName.Text = entity.UserDisplayName;
            }
        }
    }
}
