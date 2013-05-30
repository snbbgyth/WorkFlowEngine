using System.Windows;
using System.Windows.Controls;
using WorkFlowService.IDAL;
using WorkflowSetting.Help;
using WorkflowSetting.SettingForm.OperationForm;
using WorkFlowService.BLL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// ViewRoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewRoleWindow : Window
    {
        public ViewRoleWindow(IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            UserOperationDAL = userOperationDAL;
            InitRoleInfo();
            
        }

        private IUserOperationDAL UserOperationDAL { get; set; }

        private void InitRoleInfo()
        {
            DgRoleList.ItemsSource = UserOperationDAL.DataOperationInstance.QueryAll<RoleInfoModel>();
            DgRoleList.Items.Refresh();
            DgRoleList.SelectionChanged += DgRoleListSelectionChanged;
        }

        private RoleInfoModel DgRoleSelectEnity { get; set; }

        private void DgRoleListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (RoleInfoModel item in DgRoleList.SelectedItems)
            {
                DgRoleSelectEnity = item;
                break;
            }
        }

        private void RowEditClick(object sender, RoutedEventArgs e)
        {
            if (DgRoleSelectEnity == null) return;
            var editRoleWindow = new RoleRelationWindow(DgRoleSelectEnity, OperationAction.Modify, UserOperationDAL);
            editRoleWindow.ShowDialog();
        }

        private void RowDeleteClick(object sender, RoutedEventArgs e)
        {
            if (DgRoleSelectEnity != null)
            {
                // UserOperationBLL.Current.DataOperationInstance.Remove<RoleInfoModel>(DgRoleSelectEnity.Id);
            }
        }

        private void RowAddNewClick(object sender, RoutedEventArgs e)
        {
            var addRoleWindow = new RoleRelationWindow(UserOperationDAL);
            addRoleWindow.ShowDialog();
        }

        private void RowRefreshClick(object sender, RoutedEventArgs e)
        {
            InitRoleInfo();
        }
    }
}
