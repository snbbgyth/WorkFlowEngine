using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;
using WorkflowSetting.SettingForm.SelectForm;

namespace WorkflowSetting.SettingForm.OperationForm
{
    /// <summary>
    /// RoleRelationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoleRelationWindow : Window
    {
        public RoleRelationWindow()
        {
            InitializeComponent();
            InitControl();
        }

        public RoleRelationWindow(RoleInfoModel entity, OperationAction operationAction)
            : this()
        {
            UserAction = operationAction;
            InitData(entity);
            InitControl();
        }

        public RoleRelationWindow(string roleId)
            : this()
        {
            var entity = UserOperationBLL.Current.DataOperationInstance.QueryByID<RoleInfoModel>(roleId);
            InitData(entity);
        }

        private void InitData(RoleInfoModel entity)
        {
            InitProperty(entity);
            TxtRoleName.Text = entity.RoleName;
            TxtRoleDisplayName.Text = entity.RoleDisplayName;
            TxtWorkflowName.Text = entity.WorkflowName;
            TxtWorkflowDisplayName.Text = entity.WorkflowDisplayName;
            ExistUserGroupList = UserOperationBLL.Current.QueryAllUserGroupByRoleId(entity.Id);
            ExistUserInfoList = UserOperationBLL.Current.QueryAllUserInfoByRoleId(entity.Id);
            ExistActionInfoList = UserOperationBLL.Current.QueryAllActionInfoByRoleId(entity.Id);
            ClearBindData();
            LvUserGroupName.ItemsSource = ExistUserGroupList.DeepCopy();
            LvUserName.ItemsSource = ExistUserInfoList.DeepCopy();
            LvActionName.ItemsSource = ExistActionInfoList.DeepCopy();
            LvWorkflowState.ItemsSource = UserOperationBLL.Current.QueryAllWorkflowStateByRoleId(entity.Id);
        }

        private void InitControl()
        {
            if (UserAction == OperationAction.Add)
            {
                EnableControl(true);
                BtnModify.Content = "Add";
            }
            if (UserAction == OperationAction.Modify)
            {
                EnableControl(true);
                BtnModify.Content = "Modify";
            }
            if (UserAction == OperationAction.Read)
            {
                EnableControl(false);
                BtnModify.Content = "Modify";
            }
        }

        private void EnableControl(bool isEnable)
        {
            foreach (var control in SettingHelp.FindVisualChildren<Button>(this))
            {
                control.IsEnabled = isEnable;
            }
            TxtRoleDisplayName.IsReadOnly = !isEnable;
            TxtRoleName.IsReadOnly = !isEnable;
            BtnModify.IsEnabled = true;
            BtnCancel.IsEnabled = true;
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
            SettingHelp.MoidfyListByCondition(LvUserGroupName, UserOperationBLL.Current.AddUserGroupRole, UserOperationBLL.Current.DeleteUserGroupRole, ExistUserGroupList, null, Id);
        }

        private void ModifyUserInfoList()
        {
            SettingHelp.MoidfyListByCondition(LvUserName, UserOperationBLL.Current.AddUserRole, UserOperationBLL.Current.DeleteUserRole, ExistUserInfoList, null, Id);
        }

        private void ModifyActionList()
        {
            SettingHelp.MoidfyListByCondition(LvActionName, UserOperationBLL.Current.AddOperationActionInRole, UserOperationBLL.Current.DeleteOperationActionInRole, ExistActionInfoList, null, Id);
        }

        private List<UserInfoModel> ExistUserInfoList { get; set; }

        private List<UserGroupModel> ExistUserGroupList { get; set; }

        private List<OperationActionInfoModel> ExistActionInfoList { get; set; }

        private OperationAction UserAction { get; set; }

        private DateTime? CreateDateTime { get; set; }

        private bool IsDelete { get; set; }

        private string Id { get; set; }

        private void BtnAddUserGroupClick(object sender, RoutedEventArgs e)
        {
            var selectUserGroupWindow = new SelectUserGroupWindow();
            if (selectUserGroupWindow.ShowDialog() == false)
            {
                SettingHelp.AddEntityRange(LvUserGroupName, selectUserGroupWindow.SelectUserGroupList);
            }
        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {
            var selectUserWindow = new SelectUserWindow();
            if (selectUserWindow.ShowDialog() == false)
            {
               SettingHelp.AddEntityRange(LvUserName,selectUserWindow.SelectUserInfoList);
            }
        }

        private void BtnModifyClick(object sender, RoutedEventArgs e)
        {
            if (UserAction == OperationAction.Modify)
                Modify();
            if (UserAction == OperationAction.Add)
                Add();
            if (UserAction == OperationAction.Read)
                Read();
        }

        private void Read()
        {
            UserAction = OperationAction.Modify;
            InitControl();
        }

        private void Add()
        {
            var entity = GetEntity();
            if (UserOperationBLL.Current.DataOperationInstance.Insert(entity) > 0)
            {
                InitProperty(entity);
                Id = entity.Id;
                ModifyRelationList();
                LblMessage.Content = "Create successful!";
                InitControl();
            }
            else
            {
                LblMessage.Content = "Create fail";
            }
        }

        private void ModifyRelationList()
        {
            ModifyUserGroupList();
            ModifyUserInfoList();
            ModifyActionList(); 
        }

        private void InitProperty(RoleInfoModel entity)
        {
            Id = entity.Id;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = IsDelete;
        }

        private void Modify()
        {
            if (UserOperationBLL.Current.DataOperationInstance.Modify(GetEntity()) > 0)
            {
                ModifyRelationList();
                LblMessage.Content = "Modify successful.";
            }
            LblMessage.Content = "Modify fail.";
        }

        private RoleInfoModel GetEntity()
        {
            if (UserAction == OperationAction.Add)
                return new RoleInfoModel
                {
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    RoleDisplayName = TxtRoleDisplayName.Text,
                    RoleName = TxtRoleName.Text,
                    WorkflowName = TxtWorkflowName.Text,
                    WorkflowDisplayName = TxtWorkflowDisplayName.Text
                };
            if (UserAction == OperationAction.Modify)
            {
                return new RoleInfoModel
                {
                    CreateDateTime = CreateDateTime,
                    Id = Id,
                    IsDelete = IsDelete,
                    LastUpdateDateTime = DateTime.Now,
                    RoleDisplayName = TxtRoleDisplayName.Text,
                    RoleName = TxtRoleName.Text,
                    WorkflowName = TxtWorkflowName.Text,
                    WorkflowDisplayName = TxtWorkflowDisplayName.Text
                };
            }
            return new RoleInfoModel();
        }

        private void BtnAddActionClick(object sender, RoutedEventArgs e)
        {
            var selectActionWindow = new SelectActionWindow();
            if (selectActionWindow.ShowDialog() == false)
            {
                SettingHelp.AddEntityRange(LvActionName, selectActionWindow.SelectActionList);
            }
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRemoveUserGroupClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserGroupModel>(LvUserGroupName);
        }

        private void BtnRemoveUserRoleClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<UserInfoModel>(LvUserName);
        }

        private void BtnRemoveActionClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<OperationActionInfoModel>(LvActionName);
        }
    }
}
