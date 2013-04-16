using System.Windows;
using WorkFlowService.BLL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.SelectForm
{
    /// <summary>
    /// SelectUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserGroupWindow : Window
    {
        public SelectUserGroupWindow()
        {
            InitializeComponent();
            InitLvGroupInfoData();
        }

        private void InitLvGroupInfoData()
        {
            var entityList = UserOperationBLL.Current.DataOperationInstance.QueryAll<UserGroupModel>();
            LvGroupInfo.Items.Clear();
            LvGroupInfo.ItemsSource = entityList;
        }
    }
}
