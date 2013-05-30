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
    using WorkFlowService.IDAL;
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IWorkFlowEngine workFlowEngine,IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            WfeInstance = workFlowEngine;
            UserOperationDAL = userOperationDAL;
        }

        private IUserOperationDAL UserOperationDAL { get; set; }

        private IWorkFlowEngine WfeInstance { get; set; }

        private void BtnShowViewUserClick(object sender, RoutedEventArgs e)
        {
            var viewUserWindow = new ViewUserWindow(UserOperationDAL);
            viewUserWindow.Show();
        }

        private void BtnShowViewRoleClick(object sender, RoutedEventArgs e)
        {
            var viewRoleWindow = new ViewRoleWindow(UserOperationDAL);
            viewRoleWindow.Show();
        }

        private void BtnInitWorkflowClick(object sender, RoutedEventArgs e)
        {

            WfeInstance.InitWorkflowState("TestStateWorkFlow");
        }

        private void BtnShowViewActionClick(object sender, RoutedEventArgs e)
        {
            var viewActionWindow = new ViewOperationActionWindow(UserOperationDAL);
            viewActionWindow.Show();
        }

        private void BtnShowUserGroupClick(object sender, RoutedEventArgs e)
        {
            var viewUserGroupWindow = new ViewUserGroupWindow(UserOperationDAL);
            viewUserGroupWindow.Show();
        }

        private void BtnShowViewWorkflowStateClick(object sender, RoutedEventArgs e)
        {
            var viewWorkflowStateWindow = new ViewWorkflowStateWindow(UserOperationDAL);
            viewWorkflowStateWindow.Show();
        }

        private void BtnShowViewActivityClick(object sender, RoutedEventArgs e)
        {
            var viewActivityWindow = new ViewActivityWindow(UserOperationDAL);
            viewActivityWindow.Show();
        }

        private void BtnSowViewActivityLogClick(object sender, RoutedEventArgs e)
        {
            var viewActivityLogWindow = new ViewActivityLogWindow(UserOperationDAL);
            viewActivityLogWindow.Show();
        }
    }
}
