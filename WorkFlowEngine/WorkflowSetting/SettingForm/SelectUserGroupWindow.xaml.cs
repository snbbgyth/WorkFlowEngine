﻿using System;
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
    /// SelectUserGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectUserGroupWindow : Window
    {
        public SelectUserGroupWindow()
        {
            InitializeComponent();
        }

        private void InitLvGroupInfoData()
        {
            var entityList = DataOperationBLL.Current.QueryAll<UserGroupModel>();
            LvGroupInfo.Items.Clear();
            LvGroupInfo.ItemsSource = entityList;
        }
    }
}
