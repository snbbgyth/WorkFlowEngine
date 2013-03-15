using System;
using System.Collections.Generic;
using System.Windows;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// AddOperationActionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OperationActionRelationWindow : Window
    {
        public OperationActionRelationWindow()
        {
            InitializeComponent();
            InitControl();
        }

        public OperationActionRelationWindow(OperationActionInfoModel entity, OperationAction operationAction) : this()
        {
            UserAction = operationAction;
            InitData(entity);
        }

        private void InitData(OperationActionInfoModel entity)
        {
            TxtActionDisplayName.Text = entity.ActionDisplayName;
            TxtActionName.Text = entity.ActionName;
            InitProperty(entity);
            ExistUserRoleList = UserOperationBLL.Current.QueryAllRoleByActionId(entity.ID);
            LvUserRole.Items.Clear();
            LvUserRole.ItemsSource = ExistUserRoleList;
        }

        private void InitControl()
        {
            if (UserAction == OperationAction.Add)
            {
                EnableControl(true);
                BtnAdd.Content = "Add";
                Title = "AddOperationAction";
            }
            if (UserAction == OperationAction.Modify)
            {
                EnableControl(true);
                BtnAdd.Content = "Modify";
                Title = "ModifyOperationAction";
            }
            if (UserAction == OperationAction.Read)
            {
                EnableControl(false);
                BtnAdd.Content = "Modify";
                Title = "ViewOperationAction";
            }
        }

        private void EnableControl(bool isEnable)
        {
            TxtActionDisplayName.IsReadOnly = !isEnable;
            TxtActionName.IsReadOnly = !isEnable;
            BtnAddRoleName.IsEnabled = isEnable;
            BtnRemoveRoleName.IsEnabled = isEnable;
        }

        private void BtnAddRoleNameClick(object sender, RoutedEventArgs e)
        {

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
          
                LblMessage.Content = "Create successful!";
                InitControl();
            }
            else
            {
                LblMessage.Content = "Create fail";
            }
        }

        private void InitProperty(OperationActionInfoModel entity)
        {
            Id = entity.ID;
            CreateDateTime = entity.CreateDateTime;
            IsDelete = IsDelete;
        }

        private List<RoleInfoModel> ExistUserRoleList { get; set; } 

        private void ModifyOperationActionRole()
        {
            SettingHelp.MoidfyListByCondition(LvUserRole, UserOperationBLL.Current.AddUserGroupRole, UserOperationBLL.Current.DeleteUserGroupRole, ExistUserRoleList, null, Id);
        }

        private void Modify()
        {
            DataOperationBLL.Current.Modify(GetEntity());
       
        }

        private void BtnRemoveRoleNameClick(object sender, RoutedEventArgs e)
        {
            SettingHelp.RemoveItemByCondition<RoleInfoModel>(LvUserRole,new List<string>());
        }

        private OperationAction UserAction { get; set; }

        private DateTime? CreateDateTime { get; set; }

        private bool IsDelete { get; set; }

        private string Id { get; set; }

        private void ModifyEntity()
        {
            var entity = GetEntity();
            if (DataOperationBLL.Current.Modify(entity) > 1)
                LblMessage.Content = "Modify successful!";
            else
            {
                LblMessage.Content = "Modify fail!";
            }
        }

        private void AddEntity()
        {
            var entity = GetEntity();
            if (DataOperationBLL.Current.Insert(entity) > 1)
                LblMessage.Content = "Create successful!";
            else
            {
                LblMessage.Content = "Create fail!";
            }
        }

        private OperationActionInfoModel GetEntity()
        {
            if (UserAction == OperationAction.Add)
                return new OperationActionInfoModel
                {
                    CreateDateTime = DateTime.Now,
                    ActionDisplayName = TxtActionName.Text,
                    ActionName =TxtActionDisplayName.Text,
                    LastUpdateDateTime = DateTime.Now,
                };

            return new OperationActionInfoModel
            {
                CreateDateTime = CreateDateTime,
                IsDelete = IsDelete,
                ActionDisplayName = TxtActionName.Text,
                ActionName = TxtActionDisplayName.Text,
                ID = Id,
                LastUpdateDateTime = DateTime.Now
            };
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (UserAction == OperationAction.Modify)
            {
                Modify();
                ModifyOperationActionRole();
            }
            if (UserAction == OperationAction.Add)
            {
                Add();
                ModifyOperationActionRole();
            }
            if (UserAction == OperationAction.Read)
                Read();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
