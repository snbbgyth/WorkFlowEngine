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

    /// <summary>
    /// UserRelationUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserRelationUserGroupWindow : WindWindow : Window
    {
        public UserRelation       InitializeComponent();
        }

        public UserRelationUserGroupWindow(string userId)
        {
            var userInfoEntity = DataOperationBLL.Current.QueryByID<UserInfoModel>(userId);

        }

        private void InitData(UserInfoModel entity)
        {
            TxtUserId.Text = entity.ID;
            TxtUserName.Text = entity.UserName;
            TxtUserDisplayName.Text = entity.UserDisplayName;
            LvUserGroupName.Items.Clear();
         ClearDataBinding();
            ExistUueryAllUserGroupByUserId(entity.ID);
            LvUserGroupName.ItemsSource = userGroupList;
        }

        private ExistUserGroupList;
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
            var userGroupList = LvUserGroupName.ItemsSource as List<UserGroupModel>;
            if (userGroupList == null) return;
            var entityList = userGroupList.Where(entity => ExistUserGroupList.Any(t => t.ID != entity.ID));
            foreach (var entity in entityList)
            {
                UserOperationBLL.Current.AddUserInUserGroup(TxtUserId.Text, entity.ID);
            }
            var removeList = ExistUserGroupList.Where(entity => userGroupList.Any(t => t.ID != entity.ID));
            foreach (var entity in removeList)
            {
                UserOperationBLL.Current.DeleteUserInUserGroup(TxtUserId.Text, entity.ID);
            }
        }

        private void ModifyUserRoleList()
        {
            var userRoleList = LvUserGroupName.ItemsSource as List<RoleInfoModel>;
            if (userRoleList == null) return;
            var entityList = userRoleList.Where(entity =>ExistRoleInfoList.Any(t => t.ID != entity.ID));
            foreach (var entity in entityList)
            {
                UserOperationBLL.Current.AddUserInUserGroup(TxtUserId.Text, entity.ID);
            }
            var removeList = ExistRoleInfoList.Where(entity => userRoleList.Any(t => t.ID != entity.ID));
            foreach (var entity in removeList)
            {
                UserOperationBLL.Current.DeleteUserInUserGroup(TxtUserId.Text, entity.ID);
            }pClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
            //TODO: Add user to user group from SelectUserGroupWindow
        }

        private void BtnAddRoleClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
