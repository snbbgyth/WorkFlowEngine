using System.Windows;
using WorkFlowService.BLL;
using WorkFlowService.Model;
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
           DgOperationActionList.Items.Clear();
           DgOperationActionList.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<OperationActionInfoModel>();
        }

        private void BtnAddOperationActionClick(object sender, RoutedEventArgs e)
        {
            var addOperationAction = new OperationActionRelationWindow();
        }
    }
}
