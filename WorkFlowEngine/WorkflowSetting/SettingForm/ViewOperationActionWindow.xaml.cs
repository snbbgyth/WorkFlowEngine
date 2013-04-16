using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkFlowService.BLL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm
{
    using ViewForm;

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
