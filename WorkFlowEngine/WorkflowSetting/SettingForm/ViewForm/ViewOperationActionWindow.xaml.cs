using System.Windows;
using System.Windows.Controls;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Help;
using WorkflowSetting.SettingForm.OperationForm;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// ViewOperationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewOperationActionWindow : Window
    {
        public ViewOperationActionWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            DgOperationActionList.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<OperationActionInfoModel>();
            DgOperationActionList.Items.Refresh();
            DgOperationActionList.SelectionChanged += DgActionSelectionChanged;
        }

        private void DgActionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (OperationActionInfoModel item in DgOperationActionList.SelectedItems)
            {
                DgActionSelectEntity = item;
                break;
            }
        }

        private OperationActionInfoModel DgActionSelectEntity { get; set; }

        private void RowEditClick(object sender, RoutedEventArgs e)
        {
            if (DgActionSelectEntity == null) return;
            var editActionWindow = new OperationActionRelationWindow(DgActionSelectEntity, OperationAction.Modify);
            editActionWindow.ShowDialog();
        }

        private void RowDeleteClick(object sender, RoutedEventArgs e)
        {
            if (DgActionSelectEntity == null) return;
            UserOperationBLL.Current.DataOperationInstance.Remove<OperationActionInfoModel>(DgActionSelectEntity.Id);
            UserOperationBLL.Current.DeleteActionAllRoleRelation(DgActionSelectEntity.Id);
            InitData();
        }

        private void RowAddNewClick(object sender, RoutedEventArgs e)
        {
            var addActionWindow = new OperationActionRelationWindow();
            addActionWindow.Show();
        }

        private void RowRefreshClick(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}
