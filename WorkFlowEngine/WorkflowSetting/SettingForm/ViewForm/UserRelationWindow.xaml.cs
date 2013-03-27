using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;

namespace WorkflowSetting.SettingForm.ViewForm
{
    using CommonLibrary.Help;
    /// <summary>
    /// UserRelationUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserRelationWindow : Window
    {
        public UserRelationWindow()
        {
            InitializeComponent();
        }

        public UserRelationWindow(string userId, OperationAction operationAction)
            : this()
        {
            UserAction = operationAction;
            var userInfoEntity = UserOperationBLL.Current.DataOperationInstance.QueryByID<UserInfoModel>(userId);
            InitData(userInfoEntity);
        }

        public UserRelationWindow(UserInfoModel entity, OperationAction operationAction)
            : this()
        {
            UserAction = operationAction;
            InitData(entity);
            InitControl();
        }

        private void InitControl()
        {
            if (UserAction == OperationAction.Modify)
            {
                Title = "ModifyUserWindow";
                EnableControl(true);
            }
            if (UserAction == OperationAction.Read)
            {
                EnableControl(false);
            }
            VisablePassword(false);
        }

        private void VisablePassword(bool isVisable)
        {
            LblPassword.Visibility = isVisable ? Visibility.Visible : Visibility.Hidden;
            LblConfimPassword.Visibility = isVisable ? Visibility.Visible : Visibility.Hidden;
            PbPassword.Visibility = isVisable ? Visibility.Visible : Visibility.Hidden;
            PbConfimPassword.Visibility = isVisable ? Visibility.Visible : Visibility.Hidden;
            BtnCancelModifyPassword.Visibility = isVisable ? Visibility.Visible : Visibility.Hidden;
        }

        private void EnableControl(bool isEnable)
        {
            TxtUserDisplayName.IsReadOnly = !isEnable;
            TxtUserName.IsReadOnly = !isEnable;
            foreach (var control in SettingHelp.FindVisualChildren<Button>(this))
            {
                control.IsEnabled = isEnable;
            }
            foreach (var control in SettingHelp.FindVisualChildren<PasswordBox>(this))
            {
                control.IsEnabled = isEnable;
            }
            BtnModify.IsEnabled = true;
            BtnCancel.IsEnabled = true;
            BtnModifyPassword.IsEnabled = true;
        }

        private void InitData(UserInfoModel entity)
        {
            Id = entity.ID;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = entity.IsDelete;
            PbPassword.Password = entity.Password;
            TxtUserName.Text = entity.UserName;
            TxtUserDisplayName.Text = entity.UserDisplayName;
            ClearDataBinding();
            ExistUserGroupList = UserOperationBLL.Current.QueryAllUserGroupByUserId(entity.ID);
            LvUserGroupName.ItemsSource = ExistUserGroupList;
            ExistRoleInfoList = UserOperationBLL.Current.QueryAllUserRoleByUserId(entity.ID);
            LvUserRole.ItemsSource = ExistRoleInfoList;
            var reportToUserEntity = UserOperationBLL.Current.QueryReportUserInfoByUserId(entity.ID);
            if (reportToUserEntity != null)
            {
                TxtReportUserName.Text = reportToUserEntity.UserDisplayName;
                ReportToId = reportToUserEntity.ID;
                ReportRelationId = reportToUserEntity.ID;
            }
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
            if (UserAction == OperationAction.Modify)
            {
                ModifyData();
            }
            else
            {
                UserAction = OperationAction.Modify;
                InitControl();
            }
        }

        private void ModifyData()
        {
            if (!ModifyEntity()) return;
            ModifyUserGroupList();
            ModifyUserRoleList();
        }

        private void ModifyUserGroupList()
        {
            SettingHelp.MoidfyListByCondition(LvUserGroupName, UserOperationBLL.Current.AddUserInUserGroup, UserOperationBLL.Current.DeleteUserInUserGroup, ExistUserGroupList, Id);
        }

        private void ModifyUserRoleList()
        {
            SettingHelp.MoidfyListByCondition(LvUserRole, UserOperationBLL.Current.AddUserRole, UserOperationBLL.Current.DeleteUserRole, ExistRoleInfoList, Id);
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
            SettingHelp.RemoveItemByCondition<RoleInfoModel>(LvUserRole, new List<string>());
        }

        private OperationAction UserAction { get; set; }

        private DateTime? CreateDateTime { get; set; }

        private bool IsDelete { get; set; }

        private string ReportToId { get; set; }

        private string Id { get; set; }


        private string ReportRelationId { get; set; }

        private bool ModifyEntity()
        {
            if (!CheckPassword()) return false;
            var entity = GetEntity();
            
            if (UserOperationBLL.Current.DataOperationInstancey) > 1)
            {
                 ModifyReportToUser();
                LblMessage.Content = "Modify successful!";
                return true;
            }
            LblMessage.Content = "Modify fail!";
            return false;
        }

        private bool ModifyReportToUser()
        {
            if (string.IsNullOrEmpty(ReportToId)) return true;
            if (string.IsNullOrEmpty(ReportRelationId))
            {
               return  UserOperationBLL.Current.AddUserReportToUser(Id, ReportToId);
            }
            var entity = DataOperationBLL.CurreUserOperationBLL.Current.DataOperationInstanceel>(ReportRelationId);
            entity.ParentNodeID = ReportToId;
            return DataOperationBLL.CurrentUserOperationBLL.Current.DataOperationInstance      }

        private bool CheckPassword()
        {
            if (!IsModfiyPassword) return true;
            if (PbPassword.Password.CompareEqualIgnoreCase(PbConfimPassword.Password))
                return true;
            LblMessage.Content = "Please input same password";
            return false;
        }



        private UserInfodel GetEntity()
        {
            var entity = new UserInfoModel
            {
                CreateDateTime = CreateDateTime,
                IsDelete = IsDelete,
                UserName = TxtUserName.Text,
                UserDisplayName = TxtUserDisplayName.Text,
                Password = PbPassword.Password,
                ID = Id,
                LastUpdateDateTime = DateTime.Now
            };

            return entity;
        }

        private void BtnModifyPasswordClick(object sender, RoutedEventArgs e)
        {
            VisablePassword(true);
            BtnModifyPassword.IsEnabled = false;
            IsModfiyPassword = true;
        }

        private bool IsModfiyPassword { get; set; }

        private void BtnCancelModifyPasswordClick(object sender, RoutedEventArgs e)
        {
            IsModfiyPassword = false;
            BtnModifyPassword.IsEnabled = true;
            VisablePassword(false);
        }

        private void BtnAddReportUser(object sender, RoutedEventArgs e)
        {

        }

    }
}
