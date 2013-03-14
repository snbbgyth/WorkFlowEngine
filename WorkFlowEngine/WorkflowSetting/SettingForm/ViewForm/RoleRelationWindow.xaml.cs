using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Control;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;

namespace WorkflowSetting.SettingForm.ViewForm
{
   
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
        }

        public RoleRelationWindow(string roleId)
            : this()
        {
            var entity = DataOperationBLL.Current.QueryByID<RoleInfoModel>(roleId);
            InitData(entity);
        }

        private void InitData(RoleInfoModel entity)
        {
            InitProperty(entity);
            TxtRoleName.Text = entity.RoleName;
            TxtRoleDisplayName.Text = entity.RoleDisplayName;      ExistUserGroupList = UserOperationBLL.Current.QueryAllUserGroupByRoleId(entity.ID);
      
            ExistUserInfoList = UserOperationBLL.Current.QueryAllUserInfoByRoleId(entity.ID);      ExistActionInfoList = UserOperationBLL.Current.QueryAllActionInfoByRoleId(entity.ID);
      
            ClearBindData();
            LvUserGroupName.ItemsSource = ExistUserGroupList;
            LvUserName.ItemsSource = ExistUserInfoList;
            LvActionName.ItemsSource = ExistActionInfoList;      LvWorkflowState.ItemsSource = UserOperationBLL.Current.QueryAllWorkflowStateByRoleId(entity.ID);
      
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
        {      SettingHelp.MoidfyListByCondition(LvUserGroupName, UserOperationBLL.Current.AddUserGroupRole, UserOperationBLL.Current.DeleteUserGroupRole, ExistUserGroupList,null,Tx null, Id);
        }

        private void ModifyUserInfoList()
        {      SettingHelp.MoidfyListByCondition(LvUserName, UserOperationBLL.Current.AddUserRole, UserOperationBLL.Current.DeleteUserRole, ExistUserInfoList, null, TxtRoleId);
        }

        private void ModifyActionListList()
        {      SettingHelp.MoidfyListByCondition(LvActionName, UserOperationBLL.Current.AddOperationActionInRole, UserOperationBLL.Current.DeleteOperationActionInRole, ExistActionInfoList, null, TxtRoleId);
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

        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnModifyClick(object sender, RoutedEventArgs e)
        {
            if (UserAction == OperationAction.Modify)
                Modify();
            if(UserAction==OperationAction.Add)
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
            if (DataOperationBLL.Current.Insert(entity) > 0)
            {
                InitProperty(entity);
                ModifyUserGroupList();
                ModifyUserInfoList();
                ModifyActionListList();
                LblMessage.Content = "Create successful!";
                InitControl();
            }
            else
            {
                LblMessage.Content = "Create fail";
            }
        }

        private void InitProperty(RoleInfoModel entity)
        {
            Id = entity.ID;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = IsDelete;
        }

        private void Modify()
        {
            DataOperationBLL.Current.Modify(GetEntity());
            ModifyUserGroupList();
            ModifyUserInfoList();
            ModifyActionListList();
        }

        private RoleInfoModel GetEntity()
        {
            if (UserAction == OperationAction.Add)
                return new RoleInfoModel
                           {
                               CreateDateTime = DateTime.Now,
                               LastUpdateDateTime = DateTime.Now,
                               RoleDisplayName = TxtRoleDisplayName.Text,
                               RoleName = TxtRoleName.Text
                           };
            if (UserAction == OperationAction.Modify)
            {
                return new RoleInfoModel
                           {
                               CreateDateTime = CreateDateTime,
                               ID = Id,
                               IsDelete = IsDelete,
                               LastUpdateDateTime = DateTime.Now,
                               RoleDisplayName = TxtRoleDisplayName.Text,
                               RoleName = TxtRoleName.Text
                           };
            }
            return new RoleInfoModel();
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
            SettingHelp.RemoveItemByCondition<UserGroupModel>(LvUserGroupName, new List<string>());
        }
  private void BtnRemoveUserRoleClick(object sender, RoutedEventArgs e)
      
        {
            SettingHelp.RemoveItemByCondition<UserInfoModel>(LvUserName, new List<string>());
        }
  private void BtnRemoveActionClick(object sender, RoutedEventArgs e)
      
        {
            SettingHelp.RemoveItemByCondition<OperationActionInfoModel>(LvActionName, new List<string>());
        }
    }
}
