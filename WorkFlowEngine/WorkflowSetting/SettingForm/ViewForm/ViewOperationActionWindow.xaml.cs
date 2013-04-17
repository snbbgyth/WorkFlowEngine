using System.Windows;
using System.Windows.Controls;
using WorkFlowService.BLL;
using WorkFlowService.Model;
using WorkflowSetting.Helpusing WorkflowSetting.SettingForm.OperationForm;

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
           DgOperationActionLis DgOperationActionList.Items.Clear();
 nActionList.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<OperationActionInfoModel>();
        }

        private voi    DgOperationActionList.SelectionChanged += DgActionSelectionChanged;
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
            //UserOperationBLL.Current.DataOperationInstance.Remove<RoleInfoModel>(DgUserSelectEntity.Id);
        }

        private void RowAddNewClick(object sender, RoutedEventArgs e)
        {
            var addActionWindow = new OperationActionRelationWindow();
            addActionWindow.Show();
        }

 


    }
}
