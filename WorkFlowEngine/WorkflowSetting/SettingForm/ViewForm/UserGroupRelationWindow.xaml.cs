using System;
using System.Collections.Generic;
using System.Windows;
using WorkFlowService.Model;
using WorkflowSetting.Help;
using WorkFlowService.BLL;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// AddUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserGroupRelationWindow : Window
    {
        public UserGroupRelationWindow()
        {
            InitializeComponent();
        }

        public UserGroupRelationWindow(UserGroupModel entity, OperationAction operationAction)
            : this()
        {
            UserAction = operationAction;
            InitData(entity);
        }

        public UserGroupRelationWindow(string groupId, OperationAction operationAction)
            : this()
        {
            UserAction = operationAction;
            var entity = UserOperationBLL.Current.DataOperationInstance.QueryByID<UserGroupModel>(groupId);
            InitData(entity);
        }

        private void InitData(UserGroupModel entity)
        {
            TxtGroupName.Text = entity.GroupName;
            TxtGroupDisplayName.Text = entity.GroupDisplayName;
            ClearItems();
            ExistRoleInfoList = UserOperationBLL.Current.QueryAllUserRoleByUserGroupId(entity.ID);
            ExistUserInfoList = UserOperationBLL.Current.QueryAllUserInfoByUserGroupId(entity.ID);
            Id = entity.ID;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = entity.IsDelete;
            InitControl();
        }

        private void InitControl()
        {
            if (UserAction == OperationAction.Modify)
            {
                BtnAdd.Content = "Modify";
                Title = "ModifyUserGroupWindow";
                EnableControl(true);
            }
            if (UserAction == OperationAction.Read)
            {
                BtnAdd.Content = "Modify";
                Title = "ViewUserGroupWindow";
                EnableControl(false);
            }
        }

        private void EnableControl(bool isEnable)
        {
            TxtGroupDisplayName.IsReadOnly = !isEnable;
            TxtGroupName.IsReadOnly = !isEnable;
            BtnAddRoleName.IsEnabled = isEnable;
            BtnAddUser.IsEnabled = isEnable;
        }

        private void ClearItems()
        {
            LvUserRole.Items.Clear();
            LvUserName.Items.Clear();
        }

        private List<RoleInfoModel> ExistRoleInfoList { get; set; }

        private List<UserInfoModel> ExistUserInfoList { get; set; }

        private void BtnAddRoleNameClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemoveRoleNameClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<RoleInfoModel>(LvUserRole, new List<string>());
        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemoveUserClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserInfoModel>(LvUserName, new List<string>());
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (UserAction == OperationAction.Add)
            {
                AddEntity();
            }
            else if (UserAction == OperationAction.Modify)
            {
                ModifyEntity();
            }
            else
            {
                UserAction = OperationAction.Modify;
                InitControl();
            }
        }

        private OperationAction UserAction { get; set; }

        private DateTime? CreateDateTime { get; set; }

        private bool IsDelete { get; set; }

        private string Id { get; set; }


        private void ModifyEntity()
        {
            var entity = GetEntity();
            if (UserOperationBLL.Current.DataOperationInstance.Modify(entity) > 1)
                LblMessage.Content = "Modify successful!";
            else
            {
                LblMessage.Content = "Modify fail!";
            }
        }

        private void AddEntity()
        {
            var entity = GetEntity();
            if (UserOperationBLL.Current.DataOperationInstance.Insert(entity) > 1)
                LblMessage.Content = "Create successful!";
            else
            {
                LblMessage.Content = "Create fail!";
            }
        }

        private UserGroupModel GetEntity()
        {
            if (UserAction == OperationAction.Add)
                return new UserGroupModel
                {
                    CreateDateTime = DateTime.Now,
                    GroupDisplayName = TxtGroupDisplayName.Text,
                    GroupName = TxtGroupName.Text,
                    LastUpdateDateTime = DateTime.Now,
                };

            return new UserGroupModel
            {
                CreateDateTime = CreateDateTime,
                IsDelete = IsDelete,
                GroupDisplayName = TxtGroupDisplayName.Text,
                GroupName = TxtGroupName.Text,
                ID = Id,
                LastUpdateDateTime = DateTime.Now
            };
        }


    }
}
