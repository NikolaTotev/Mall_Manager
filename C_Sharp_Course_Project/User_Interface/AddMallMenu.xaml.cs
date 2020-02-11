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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for AddMallMenu.xaml
    /// </summary>
    public partial class AddMallMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private IAppView m_NextView;
        public AddMallMenu()
        {
            InitializeComponent();
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousView)
        {
            m_PreviousView = previousView;
        }

        public void SetNextView(IAppView nextView)
        {
            m_NextView = nextView;
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {

            m_CurrentMainWindow.ChangeViewForward(m_PreviousView, this);
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            Mall mallToAdd = new Mall(Guid.NewGuid(), Tb_MallName.Text, Tb_MallDesc.Text);
            MallManager.GetInstance().AddMall(mallToAdd);
            Dashboard newDash = new Dashboard();
            newDash.SetPreviousView(this);
            newDash.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ChangeViewForward(newDash, this);
        }

        private void Tb_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_MallName.Text == "Mall Name")
            {
                Tb_MallName.Text = "";
            }
        }

        private void Tb_Desc_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_MallDesc.Text == "Mall Description")
            {
                Tb_MallDesc.Text = "";
            }
        }

        private void Tb_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_MallName.Text))
            {
                Tb_MallName.Text = "Mall Description";
            }
        }

        private void Tb_MallDesc_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_MallDesc.Text))
            {
                Tb_MallDesc.Text = "Mall Description";
            }
        }

        private void Tb_MallName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        private void Tb_MallDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }
        private void ValidateInput()
        {
            if (Tb_MallDesc != null && Tb_MallName != null && Btn_Add != null)
            {
                if (Tb_MallName.Text != "Mall name" && !string.IsNullOrEmpty(Tb_MallName.Text) && Tb_MallDesc.Text != "Mall Description" && !string.IsNullOrEmpty(Tb_MallDesc.Text))
                {
                    Btn_Add.IsEnabled = true;
                }
                else
                {
                    Btn_Add.IsEnabled = false;
                }
            }
        }
    }
}
