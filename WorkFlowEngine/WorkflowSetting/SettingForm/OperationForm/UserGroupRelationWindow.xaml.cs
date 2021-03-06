﻿using System;
using System.Collections.Generic;
using System.Windows;
using WorkFlowService.IDAL;
using WorkFlowService.Model;
using WorkflowSetting.Help;
using WorkFlowService.BLL;
using WorkflowSetting.SettingForm.SelectForm;

namespace WorkflowSetting.SettingForm.OperationForm
{
    /// <summary>
    /// AddUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserGroupRelationWindow : Window
    {
        public UserGroupRelationWindow(IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            UserOperationDAL = userOperationDAL;
            InitControl();
            
        }

        public UserGroupRelationWindow(UserGroupModel entity, OperationAction operationAction, IUserOperationDAL userOperationDAL)
            : this(userOperationDAL)
        {
            UserAction = operationAction;
            InitData(entity);
            InitControl();
        }

        public UserGroupRelationWindow(string groupId, OperationAction operationAction,IUserOperationDAL userOperationDAL)
            : this(userOperationDAL)
        {
            UserAction = operationAction;
            var entity = UserOperationDAL.DataOperationInstance.QueryByID<UserGroupModel>(groupId);
            InitData(entity);
        }

        private IUserOperationDAL UserOperationDAL { get; set; }

        private void InitData(UserGroupModel entity)
        {
            TxtGroupName.Text = entity.GroupName;
            TxtGroupDisplayName.Text = entity.GroupDisplayName;
            ClearItems();
            ExistRoleInfoList = UserOperationDAL.QueryAllUserRoleByUserGroupId(entity.Id);
            ExistUserInfoList = UserOperationDAL.QueryAllUserInfoByUserGroupId(entity.Id);
            LvGroupRole.ItemsSource = ExistRoleInfoList.DeepCopy();
            LvUserName.ItemsSource = ExistUserInfoList.DeepCopy();
            Id = entity.Id;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = entity.IsDelete;

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
            LvGroupRole.Items.Clear();
            LvUserName.Items.Clear();
        }

        private List<RoleInfoModel> ExistRoleInfoList { get; set; }

        private List<UserInfoModel> ExistUserInfoList { get; set; }

        private void BtnAddRoleNameClick(object sender, RoutedEventArgs e)
        {
            var selectRoleWindow = new SelectRoleWindow(UserOperationDAL);
            if (selectRoleWindow.ShowDialog() == false)
            {
                SettingHelp.AddEntityRange(LvGroupRole, selectRoleWindow.SelectRoleInfoList);
            }
        }

        private void BtnRemoveRoleNameClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<RoleInfoModel>(LvGroupRole);
        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {
            var selectUserWindow = new SelectUserWindow(UserOperationDAL);
            if (selectUserWindow.ShowDialog() == false)
            {
                SettingHelp.AddEntityRange(LvUserName, selectUserWindow.SelectUserInfoList);
            }
        }

        private void BtnRemoveUserClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserInfoModel>(LvUserName);
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ModifyUserNameList()
        {
            SettingHelp.MoidfyListByCondition(LvUserName, UserOperationDAL.AddUserInUserGroup, UserOperationDAL.DeleteUserInUserGroup, ExistUserInfoList, null, Id);
        }

        private void ModifyGroupRoleList()
        {
            SettingHelp.MoidfyListByCondition(LvGroupRole, UserOperationDAL.AddUserGroupRole, UserOperationDAL.DeleteUserGroupRole, ExistRoleInfoList, Id);
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
            if (UserOperationDAL.DataOperationInstance.Modify(entity) > 0)
            {
                ModifyRelationList();
                LblMessage.Content = "Modify successful!";
            }
            else
                LblMessage.Content = "Modify fail!";
        }

        private void AddEntity()
        {
            var entity = GetEntity();
            if (UserOperationDAL.DataOperationInstance.Insert(entity) > 0)
            {
                Id = entity.Id;
                ModifyRelationList();
                UserAction = OperationAction.Modify;
                InitControl();
                LblMessage.Content = "Create successful!";
            }
            else
                LblMessage.Content = "Create fail!";
        }

        private void ModifyRelationList()
        {
            ModifyUserNameList();
            ModifyGroupRoleList();
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
                Id = Id,
                LastUpdateDateTime = DateTime.Now
            };
        }
    }
}
