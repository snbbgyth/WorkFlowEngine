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
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.SelectForm
{
    /// <summary>
    /// SelectUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserWindow : Window
    {
        public SelectUserWindow()
        {
            InitializeComponent();
            InitLvData();
        }

        private void InitLvData()
        {
            SelectUserInfoList = new List<UserInfoModel>();
             var entityList = UserOperationBLL.Current.DataOperationInstance.QueryAll<UserInfoModel>();
            LvUserInfo.Items.Clear();
            LvUserInfo.ItemsSource = entityList;
            LvUserInfo.SelectionChanged += LvUserInfoSelectionChanged;
        }

        public List<UserInfoModel> SelectUserInfoList;

        private void LvUserInfoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectUserInfoList.Clear();
            foreach (UserInfoModel item in LvUserInfo.SelectedItems)
            {
               SelectUserInfoList.Add(item);
            }
        }

        private void BtnSelectClick(object sender, RoutedEventArgs e)
        {
            OnSelectComplete();
            Close();
        }

        public event EventHandler SelectComplete;

        protected virtual void OnSelectComplete()
        {
            EventHandler handler = SelectComplete;
            if (handler != null) handler(this, EventArgs.Empty);
        }

   
    }
}
