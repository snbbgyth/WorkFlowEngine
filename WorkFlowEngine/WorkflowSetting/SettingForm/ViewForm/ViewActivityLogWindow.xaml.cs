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
using CommonLibrary.Model;
using WorkFlowService.BLL;
using WorkflowSetting.Help;

namespace WorkflowSetting.SettingForm.ViewForm
{
    /// <summary>
    /// ViewActivityLogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewActivityLogWindow : Window
    {
        public ViewActivityLogWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            var entityList = UserOperationBLL.Current.DataOperationInstance.QueryAll<WorkFlowActivityLogModel>();
            DgActivityList.ItemsSource = entityList;
            DgActivityList.Items.Refresh();
            CbWorkflowName.ItemsSource = entityList.Select(entity => entity.WorkflowName).Distinct();
            CbWorkflowName.Items.Refresh();
            CbQueryType.ItemsSource = SettingHelp.ActivityQueryTypeList();
            CbQueryType.Items.Refresh();
        }

        private void BtnQueryClick(object sender, RoutedEventArgs e)
        {
            DgActivityList.ItemsSource =
                UserOperationBLL.Current.QueryActivityLogByCondition(
                new KeyValuePair<string, string>("WorkflowName", CbWorkflowName.SelectedItem == null ? null : CbWorkflowName.SelectedItem.ToString()),
                new KeyValuePair<string, object>(CbQueryType.SelectedValue==null?null:CbQueryType.SelectedValue.ToString(), TxtQueryValue.Text.Trim()));
            DgActivityList.Items.Refresh();
        }
    }
}
