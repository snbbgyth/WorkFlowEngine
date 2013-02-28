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

namespace WorkflowSetting.SettingForm
{

    using WorkFlowService.BLL;
    using WorkFlowService.Model;

    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class ViewUserWindow : Window
    {
        public ViewUserWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            //DgUserList.Items.Clear();

            DgUserList.ItemsSource = DataOperationBLL.Current.QueryAll<UserInfoModel>();

        }

        private void BtnAddUserClick(object sender, RoutedEventArgs e)
        {

            var addUserWindow = new OperationUserWindow();
            addUserWindow.Show();
        }

        private void BtnRelationUserGroup(object sender, RoutedEventArgs e)
        {

        }

    }
}
