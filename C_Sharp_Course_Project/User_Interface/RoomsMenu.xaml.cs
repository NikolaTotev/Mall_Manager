﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// Interaction logic for RoomsMenu.xaml
    /// </summary>
    public partial class RoomsMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        public RoomsMenu()
        {
            InitializeComponent();
            Lbl_MallName.Content = MallManager.GetInstance().CurrentMall.Name;
            LoadRentalSpaces();
        }

        public void LoadRentalSpaces()
        {
            Lv_RentalSpaces.Items.Clear();
            foreach (var rentalSpace in RoomManager.GetInstance().Rooms)
            {
                ListViewItem itemToAdd = new ListViewItem();
                itemToAdd.Tag = rentalSpace.Key;
                
                if (rentalSpace.Value is Room currentRoom)
                {
                    itemToAdd.Content = currentRoom.Name;
                }
                Lv_RentalSpaces.Items.Add(itemToAdd);
            }
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Btn_AddRentalSpace_Click(object sender, RoutedEventArgs e)
        {
            AddRoomMenu addRoom = new AddRoomMenu();
            m_CurrentMainWindow.ChangeView(addRoom, this);
        }

        private void Lv_RentalSpaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lv_RentalSpaces.SelectedIndex >= 0 && Lv_RentalSpaces.SelectedIndex < Lv_RentalSpaces.Items.Count)
            {
                if (Lv_RentalSpaces.SelectedItem is ListViewItem currentSelection)
                {
                    RoomPage room = new RoomPage((Guid)currentSelection.Tag);
                    room.SetMainWindow(m_CurrentMainWindow);
                    room.SetPreviousView(this);
                    m_CurrentMainWindow.ChangeView(room, this);
                }
                
            }
        }
    }
}
