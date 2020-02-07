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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for RoomInfoPage.xaml
    /// </summary>
    public partial class RoomInfoPage : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private Room m_CurrentRoom;
        public RoomInfoPage(Room currentRoom)
        {
            InitializeComponent();
            m_CurrentRoom = currentRoom;
            Tb_Name.Text = m_CurrentRoom.Name;
            Tb_Desc.Text = m_CurrentRoom.Description;
            Lb_DateCreated.Content = m_CurrentRoom.CreateDate.ToShortDateString();
            Lb_LastEdited.Content= m_CurrentRoom.LastEditDate.ToShortDateString();
        }

        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Tb_Desc_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Tb_Name_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
