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
            ReloadMallInfo();
            ReloadConfigInfo();
        }

        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_Name.Text))
            {
                SerializationManager.RenameFiles(MallManager.GetInstance().CurrentMallName, Tb_Name.Text);
                Mall editedMall = new Mall(MallManager.GetInstance().CurrentMall.Id, Tb_Name.Text, Tb_Desc.Text);
                MallManager.GetInstance().EditMall(editedMall);
            }

        }

        private void Btn_ClearRooms_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to execute this action?", "Clear all activities", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    RoomManager.GetInstance().ClearRooms();
                    ReloadMallInfo();
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
                    ReloadMallInfo();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void ReloadMallInfo()
        {
            Lb_ActivityNumberValue.Content = ActivityManager.GetInstance().Activities.Count.ToString();
            Lb_RoomNumberValue.Content = RoomManager.GetInstance().Rooms.Count.ToString();
        }


        private void ReloadConfigInfo()
        {
            Lv_CurrentActivityTypes.ItemsSource = ActivityManager.GetInstance().GetCategories();
            Lv_CurrentRoomTypes.ItemsSource = RoomManager.GetInstance().GetRoomTypes();
        }
        private void Btn_AddRoomType_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Tb_RoomType.Text) && !RoomManager.GetInstance().GetRoomTypes().Contains(Tb_RoomType.Text))
            {
                RoomManager.GetInstance().AddRoomType(Tb_RoomType.Text);
                ReloadConfigInfo();
            }
            else
            {
                Tb_RoomType.Background = (Brush)Application.Current.Resources["InputError"];

            }
        }

        private void Btn_RemoveRoomType_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_CurrentRoomTypes.SelectedItem != null)
            {
                string selectedItem = Lv_CurrentRoomTypes.SelectedItem.ToString();
                RoomManager.GetInstance().RemoveRoomType(selectedItem);
                ReloadConfigInfo();
            }
        }

        private void Btn_RemoveActivityType_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_CurrentActivityTypes.SelectedItem != null)
            {
                string selectedItem = Lv_CurrentActivityTypes.SelectedItem.ToString();
                ActivityManager.GetInstance().RemoveCategory(selectedItem);
                ReloadConfigInfo();
            }
        }

        private void Btn_AddActivityType_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Tb_ActivityType.Text) && !ActivityManager.GetInstance().GetCategories().Contains(Tb_ActivityType.Text))
            {
                ActivityManager.GetInstance().AddNewCategory(Tb_ActivityType.Text);
                ReloadConfigInfo();
            }
            else
            {
                Tb_RoomType.Background = (Brush)Application.Current.Resources["InputError"];
            }
        }


        private void Btn_MallSettings_OnClick(object sender, RoutedEventArgs e)
        {
            Grd_MallSettings.Visibility = Visibility.Visible;
            Grd_RoomTypesConfig.Visibility = Visibility.Hidden;
            Grd_ActivityTypesConfig.Visibility = Visibility.Hidden;
        }

        private void Btn_RoomConfigs_OnClick(object sender, RoutedEventArgs e)
        {
            Grd_MallSettings.Visibility = Visibility.Hidden;
            Grd_RoomTypesConfig.Visibility = Visibility.Visible;
            Grd_ActivityTypesConfig.Visibility = Visibility.Hidden;
        }

        private void Btn_ActivityConfigs_OnClick(object sender, RoutedEventArgs e)
        {
            Grd_MallSettings.Visibility = Visibility.Hidden;
            Grd_RoomTypesConfig.Visibility = Visibility.Hidden;
            Grd_ActivityTypesConfig.Visibility = Visibility.Visible;
        }
    }
}
