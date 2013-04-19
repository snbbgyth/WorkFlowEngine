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

namespace WorkflowSetting.SettingForm.ViewForm
{
    using WorkFlowService.BLL;
    using WorkFlowService.Model;
    using WorkflowSetting.Help;
    using CommonLibrary.Model;

    /// <summary>
    /// ViewActivityWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewActivityWindow : Window
    {
        public ViewActivityWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            var entityList = UserOperationBLL.Current.DataOperationInstance.QueryAll<WorkFlowActivityModel>();
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
                UserOperationBLL.Current.QueryActivityByCondition(
                new KeyValuePair<string, string>("WorkflowName", CbWorkflowName.SelectedItem == null ? null : CbWorkflowName.SelectedItem.ToString()),
                    new KeyValuePair<string, object>(CbQueryType.SelectedValue.ToString(), TxtQueryValue.Text.Trim()));
            DgActivityList.Items.Refresh();
        }
    }
}
