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
    public partial class UserRelationUserGroupWindow : Window
    {
        public UserRelationUserGroupWindow()
        {
            InitializeComponent();
        }

        public UserRelationUserGroupWindow(string userId) : this()
        {
            var userInfoEntity = DataOperationBLL.Current.QueryByID<UserInfoModel>(userId);

        }

        private void InitData(UserInfoModel entity)
        {
            TxtUserId.Text = entity.ID;
            TxtUserName.Text = entity.UserName;
            TxtUserDisplayName.Text = entity.UserDisplayName;
            LvUserGroupName.Items.Clear();
            var userGroupList = UserOperationBLL.Current.QueryAllUserGroupByUserId(entity.ID);
            LvUserGroupName.ItemsSource = userGroupList;
        }

        private void BtnModifyClick(object sender, RoutedEventArgs e)
        {
          
        }

        private void BtnAddUserGroupClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
