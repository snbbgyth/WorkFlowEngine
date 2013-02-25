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

namespace WorkflowSetting
{
    using WorkFlowService.BLL;
    using WorkFlowService.Model;
    using WorkFlowService.Help;
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
           var userId = UserOperationBLL.Current.LoginIn(TxtUserName.Text.Trim(), TxtPassword.Text.Trim());

            if (!string.IsNullOrEmpty(userId))
                WFUntilHelp.UserId = userId;
            else
             LblErrorMessage.Content ="User name or password error.";
        }

 

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
