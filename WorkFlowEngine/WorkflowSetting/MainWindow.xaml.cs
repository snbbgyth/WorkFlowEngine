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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkflowSetting.SettingForm.ViewForm;

namespace WorkflowSetting
{
    using SettingForm;
    using WorkFlowService.BLL;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnShowViewUserClick(object sender, RoutedEventArgs e)
        {
            var viewUserWindow = new ViewUserWindow();
            viewUserWindow.Show();
        }

        private void BtnShowViewRoleClick(object sender, RoutedEventArgs e)
        {
            var viewRoleWindow = new ViewRoleWindow();
            viewRoleWindow.Show();
        }

        private void BtnInitWorkflowClick(object sender, RoutedEventArgs e)
        {
           
            WorkFlowEngine.Current.InitWorkflowState("TestStateWorkFlow");
        }
  

        private void BtnShowViewActionClick(object sender, RoutedEventArgs e)
        {
            var viewActionWindow = new ViewOperationActionWindow();
            viewActionWindow.Show();
        }

        private void BtnShowUserGroupClick(object sender, RoutedEventArgs e)
        {
            var viewUserGroupWindow = new ViewUserGroupWindow();
            viewUserGroupWindow.Show();
        }

        private void BtnShowViewWorkflowStateClick(object sender, RoutedEventArgs e)
        {
            var viewWorkflowStateWindow = new ViewWorkflowStateWindow();
            viewWorkflowStateWindow.Show();
        }
    }
}
