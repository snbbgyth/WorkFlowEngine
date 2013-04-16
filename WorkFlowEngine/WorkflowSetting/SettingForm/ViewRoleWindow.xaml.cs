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
using WorkflowSetting.Help;

namespace WorkflowSetting.SettingForm
{
    using WorkFlowService.BLL;
    using WorkFlowService.Model;
    using ViewForm;

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
