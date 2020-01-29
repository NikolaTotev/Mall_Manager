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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl, IAppView
    {
        private ProgramManager CurrentManager;
        private MainWindow CurrentMainWindow;
        public Dashboard()
        {
            InitializeComponent();
            LoadMallList();
        }

        public void LoadMallList()
        {
            string mallName = MallManager.GetInstance().GetMalls().Values.ToList()[0].Split()[0];
            BigButton newBigButton = new BigButton("pack://application:,,,/Resources/Icons/StoreFront_Icon.png", mallName);
            Lv_Malls.Items.Add(newBigButton);
        }

        private void OnClick_Add(object sender, RoutedEventArgs e)
        {
            AddMallMenu addMall = new AddMallMenu("MallName");
            CurrentMainWindow.ChangeView(addMall);
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            CurrentMainWindow = currentWindow;
        }
    }
}
