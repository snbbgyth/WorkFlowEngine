using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CommonLibrary.IDAL;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;
using WorkflowSetting.SettingForm.SelectForm;

namespace WorkflowSetting.SettingForm.ViewForm
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
            var entity = UserOperationBLL.Current.DataOperationInstance.QueryByID<RoleInfoModel>(roleId);
            InitData(entity);
        }

        private void InitData(RoleInfoModel entity)
        {
            InitProperty(entity);
            TxtRoleName.Text = entity.RoleName;
            TxtRoleDisplayName.Text = entity.RoleDisplayName;
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
            selectUserGroupWindow.ShowDialog();
        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {
            var selectUserWindow = new SelectUserWindow();
            if (selectUserWindow.ShowDialog() == false)
            {
                //if (selectUserWindow.SelectUserInfoList != null)
                //{
                //    List<UserInfoModel> entityList;
                //    entityList = LvUserName.ItemsSource as List<UserInfoModel>;
                //    if (entityList == null) entityList = new List<UserInfoModel>();
                //    entityList.AddRange(entityList);

                //    LvUserName.Items.Clear();
                //    LvUserName.ItemsSource = selectUserWindow.SelectUserInfoList;
                //}
                AddEntityRange(LvUserName,selectUserWindow.SelectUserInfoList);
            }
        }

        private void AddEntityRange<T>(ListView lv, List<T> selectList)where T: ITableModel
        {
            if (selectList == null) return;
            List<T> entityList;
            entityList = lv.ItemsSource as List<T>;
            if (entityList == null) entityList = new List<T>();
            entityList.AddRange(selectList.FindAll(entity=>!entityList.Exists(item=>item.Id== entity.Id)));
   

            lv.ItemsSource = entityList;
            lv.Items.Refresh();
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
                ModifyUserGroupList();
                ModifyUserInfoList();
                ModifyActionList();
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
            Id = entity.Id;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = IsDelete;
        }

        private void Modify()
        {
            UserOperationBLL.Current.DataOperationInstance.Modify(GetEntity());
            ModifyUserGroupList();
            ModifyUserInfoList();
            ModifyActionList();
            LblMessage.Content = "Modify successful.";
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
                    Id = Id,
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
            var entityList = LvUserName.ItemsSource as List<UserInfoModel>;
            foreach (UserInfoModel item in LvUserName.SelectedItems)
            {
                if (entityList != null) entityList.Remove(item);
            }
            LvUserName.ItemsSource = entityList;
            LvUserName.Items.Refresh();
            // SettingHelp.RemoveItemByCondition<UserInfoModel>(LvUserName, LvUserName.SelectedItems as List<UserInfoModel>);
        }

        private void BtnRemoveActionClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<OperationActionInfoModel>(LvActionName, new List<string>());
        }
    }
}
