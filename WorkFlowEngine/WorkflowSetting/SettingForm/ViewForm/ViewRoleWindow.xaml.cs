using System.Windows;
using System.Windows.Controls;
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
        public ViewRoleWindow()
        {
            InitializeComponent();
            InitRoleInfo();
        }

        private void InitRoleInfo()
        {
            DgRoleList.Items.Clear();
            DgRoleList.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<RoleInfoModel>();
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
            var editRoleWindow = new RoleRelationWindow(DgRoleSelectEnity, OperationAction.Modify);
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
            var addRoleWindow = new RoleRelationWindow();
            addRoleWindow.ShowDialog();
        }
    }
}
