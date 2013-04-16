using System.Windows;
using WorkFlowService.BLL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.SelectForm
{
    /// <summary>
    /// SelectRoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectRoleWindow : Window
    {
        public SelectRoleWindow()
        {
            InitializeComponent();
            InitRoleInfoList();
        }

        private void InitRoleInfoList()
        {
            LvRoleInfo.Items.Clear();
            LvRoleInfo.ItemsSource = UserOperationBLL.Current.DataOperationInstance.QueryAll<RoleInfoModel>();
        }
    }
}
