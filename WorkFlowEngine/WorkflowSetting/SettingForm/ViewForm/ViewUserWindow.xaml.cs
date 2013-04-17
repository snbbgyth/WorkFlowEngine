using System.Windows;
using System.Windows.Controls;
using WorkflowSetting.Help;
using WorkflowSetting.SettingForm.OperationForm;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.SettingForm.AddForm;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class ViewUserWindow : Window
    {
        public ViewUserWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            DgUserList.Items.Clear();
            DgUserList.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<UserInfoModel>();
            DgUserList.SelectionChanged+=DgUserListSelectionChanged;
        }

        private void DgUserListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (UserInfoModel item in DgUserList.SelectedItems)
            {
                DgUserSelectEntity = item;
                break;
            }
        }

        private UserInfoModel DgUserSelectEntity { get; set; }

        private void RowEditClick(object sender, RoutedEventArgs e)
        {
            if (DgUserSelectEntity == null) return;
            var editRoleWindow = new UserRelationWindow(DgUserSelectEntity, OperationAction.Modify);
            editRoleWindow.ShowDialog();
        }

        private void RowDeleteClick(object sender, RoutedEventArgs e)
        {
            if (DgUserSelectEntity == null) return;
            //UserOperationBLL.Current.DataOperationInstance.Remove<RoleInfoModel>(DgUserSelectEntity.Id);
        }

        private void RowAddNewClick(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUserWindow();
            addUserWindow.Show();
        }

 
    }
}
