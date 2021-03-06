﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorkFlowService.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Model;

namespace WorkflowSetting.SettingForm.SelectForm
{
    /// <summary>
    /// SelectRoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectRoleWindow : Window
    {
        public SelectRoleWindow(IUserOperationDAL userOperationDAL)
        {
            InitializeComponent();
            UserOperationDAL = userOperationDAL;
            InitLvData();
           
        }

        private void InitLvData()
        {
            SelectRoleInfoList = new List<RoleInfoModel>();
            LvRoleInfo.Items.Clear();
            LvRoleInfo.ItemsSource = UserOperationDAL.DataOperationInstance.QueryAll<RoleInfoModel>();
            LvRoleInfo.SelectionChanged += LvRoleInfoSelectionChanged;
        }

        private IUserOperationDAL UserOperationDAL { get; set; }

        public List<RoleInfoModel> SelectRoleInfoList;

        private void LvRoleInfoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectRoleInfoList.Clear();
            foreach (RoleInfoModel item in LvRoleInfo.SelectedItems)
            {
                SelectRoleInfoList.Add(item);
            }
        }

        private void BtnSelectClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            SelectRoleInfoList = null;
            Close();
        }

 
    }
}
