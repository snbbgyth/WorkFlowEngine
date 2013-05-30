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
using WorkFlowService.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.SelectForm
{
    /// <summary>
    /// SelectUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserWindow : Window
    {
        public SelectUserWindow(IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            UserOperationDAL = userOperationDAL;
            InitLvData();
            
        }

        public SelectUserWindow(int selectCount, IUserOperationDAL userOperationDAL)
            : this(userOperationDAL)
        {
            SelectCount = selectCount;
        }

        private int SelectCount { get; set; }

        private IUserOperationDAL UserOperationDAL { get; set; }

        private void InitLvData()
        {
            SelectUserInfoList = new List<UserInfoModel>();
            var entityList = UserOperationDAL.DataOperationInstance.QueryAll<UserInfoModel>();
            LvUserInfo.Items.Clear();
            LvUserInfo.ItemsSource = entityList;
            LvUserInfo.SelectionChanged += LvUserInfoSelectionChanged;
        }

        public List<UserInfoModel> SelectUserInfoList;

        private void LvUserInfoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectUserInfoList.Clear();
            if (!CheckSelectCount()) return;
            foreach (UserInfoModel item in LvUserInfo.SelectedItems)
            {
                SelectUserInfoList.Add(item);
            }
        }

        private bool CheckSelectCount()
        {
            if (SelectCount > 0 && LvUserInfo.SelectedItems.Count > SelectCount)
            {
                LblMessage.Content = string.Format("Please select {0} user.", SelectCount);
                return false;
            }
            LblMessage.Content = string.Empty;
            return true;
        }

        private void BtnSelectClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            SelectUserInfoList = null;
            Close();
        }

    }
}
