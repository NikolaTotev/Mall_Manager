using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
        private IAppView m_NextView;
        private readonly List<RoomListItem> m_Rooms;
        public RoomsMenu()
        {
            InitializeComponent();
            Tbl_HeaderText.Text = MallManager.GetInstance().CurrentMall.Name + " - Rental Spaces";

            Lv_RentalSpaces.SelectionMode = SelectionMode.Multiple;
            m_Rooms = new List<RoomListItem>();
            foreach (Room room in RoomManager.GetInstance().Rooms.Values.ToList())
            {
                RoomListItem itemToAdd = new RoomListItem
                {
                    RoomId = room.Id,
                    RoomName = room.Name,
                    Description = room.Description,
                    RoomFloor = room.Floor.ToString(),
                    RoomNumber = room.RoomNumber.ToString(),
                    DateCreated = room.CreateDate.ToShortDateString(),
                    LastEdited = room.LastEditDate.ToShortDateString()
                };
                m_Rooms.Add(itemToAdd);
            }
            DataContext = m_Rooms;
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

        public void SetPreviousView(IAppView previousView)
        {
            m_PreviousView = previousView;
        }

        public void SetNextView(IAppView nextView)
        {
            m_NextView = nextView;
        }

        public IAppView GetPreviousView()
        {
            return m_PreviousView;
        }

        private void Btn_AddRentalSpace_Click(object sender, RoutedEventArgs e)
        {
            AddRoomMenu addRoom = new AddRoomMenu();
            m_CurrentMainWindow.ChangeViewForward(addRoom, this);
        }

        private void Lv_RentalSpaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Lv_RentalSpaces.SelectedIndex >= 0 && Lv_RentalSpaces.SelectedIndex < Lv_RentalSpaces.Items.Count)
            //{
            //    if (Lv_RentalSpaces.SelectedItem is ListViewItem currentSelection)
            //    {
            //        SpacePage space = new SpacePage((Guid)currentSelection.Tag);
            //        space.SetMainWindow(m_CurrentMainWindow);
            //        space.SetPreviousView(this);
            //        m_CurrentMainWindow.ChangeViewForward(space, this);
            //    }

            //}
        }

        private void Btn_DeleteRentalSpace_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_RentalSpaces.SelectedItems.Count <= 0) return;
            for (var i = Lv_RentalSpaces.SelectedItems.Count - 1; i >= 0; i--)
            {
                RoomListItem itemToDelete = (RoomListItem)Lv_RentalSpaces.SelectedItems[i];
                RoomManager.GetInstance().DeleteRoom(itemToDelete.RoomId, MallManager.GetInstance().CurrentMall.Name);
                m_Rooms.Remove(itemToDelete);
                Lv_RentalSpaces.Items.Refresh();
            }
        }

        private void Btn_SelectAll_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (RoomListItem item in Lv_RentalSpaces.Items)
            {
                item.IsSelected = true;
            }
        }

        private void Btn_DeselectAllSelected_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (RoomListItem item in Lv_RentalSpaces.Items)
            {
                item.IsSelected = false;
            }
        }

        private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeViewBackward(m_PreviousView, this);
        }


        private void RoomItem_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RoomListItem item = ((ListViewItem)sender).Content as RoomListItem;
            if (item == null)
            {
                return;
            }
            SpacePage space = new SpacePage(item.RoomId);
            m_CurrentMainWindow.ChangeViewForward(space, this);
        }
    }
}
