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
            InitData();
        }

        private void InitData()
        {
            ExistUserName = string.Empty;
            ExistPassword = string.Empty;
            if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()))
                ExistUserName = TxtUserName.Text.Trim();
            if (!string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                ExistPassword = TxtPassword.Text.Trim();
        }

        private string ExistUserName { get; set; }

        private string ExistPassword { get; set; }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            if (!CheckInput()) return;
            var userId = UserOperationBLL.Current.LoginIn(TxtUserName.Text.Trim(), TxtPassword.Text.Trim());
            if (!string.IsNullOrEmpty(userId))
                WFUntilHelp.UserId = userId;
            else
                LblErrorMessage.Content = "User name or password error.";
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(TxtUserName.Text.Trim()) || string.IsNullOrEmpty(TxtPassword.Text.Trim()) ||
                string.CompareOrdinal(TxtUserName.ToString(), TxtUserName.Text.Trim()) == 0 || 
                string.CompareOrdinal(TxtPassword.ToString() ,TxtPassword.Text.Trim())==0)
            {
                LblErrorMessage.Content = "User name or password error.";
                return false;
            }
            return true;
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
