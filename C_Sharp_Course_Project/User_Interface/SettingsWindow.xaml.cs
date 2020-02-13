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
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Tb_Name.Text = MallManager.GetInstance().CurrentMall.Name;
            Tb_Desc.Text = MallManager.GetInstance().CurrentMall.Description;
            ReloadInfo();
        }

        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            MallManager.GetInstance().EditMall(MallManager.GetInstance().CurrentMall.Id, Tb_Name.Text, Tb_Desc.Text);
        }

        private void Btn_ClearRooms_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to execute this action?", "Clear all activities", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    RoomManager.GetInstance().ClearRooms();
                    ReloadInfo();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Btn_ClearActivities_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to execute this action?", "Clear all activities", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ActivityManager.GetInstance().ClearActivities();
                    ReloadInfo();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void ReloadInfo()
        {
            Lb_ActivityNumberValue.Content = ActivityManager.GetInstance().Activities.Count.ToString();
            Lb_RoomNumberValue.Content = RoomManager.GetInstance().Rooms.Count.ToString();
        }
    }
}
