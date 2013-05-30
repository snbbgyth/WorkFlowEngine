using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkFlowService.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.SelectForm
{
    /// <summary>
    /// SelectUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserGroupWindow : Window
    {
        public SelectUserGroupWindow(IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            UserOperationDAL = userOperationDAL;
            InitData();
            
        }

        private void InitData()
        {
            InitLvGroupInfoData();
            SelectUserGroupList = new List<UserGroupModel>();
        }

        private IUserOperationDAL UserOperationDAL { get; set; }

        private void InitLvGroupInfoData()
        {
            var entityList = UserOperationDAL.DataOperationInstance.QueryAll<UserGroupModel>();
            LvGroupInfo.Items.Clear();
            LvGroupInfo.ItemsSource = entityList;
            LvGroupInfo.SelectionChanged+=LvGroupInfoSelectionChanged;
        }

        private void LvGroupInfoSelectionChanged(object sender,  SelectionChangedEventArgs e)
        {
            SelectUserGroupList.Clear();
            foreach (UserGroupModel item in LvGroupInfo.SelectedItems)
            {
                SelectUserGroupList.Add(item);
            }
        }

        public List<UserGroupModel> SelectUserGroupList; 

        private void BtnSelectClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            SelectUserGroupList = null;
            Close();
        }
    }
}
