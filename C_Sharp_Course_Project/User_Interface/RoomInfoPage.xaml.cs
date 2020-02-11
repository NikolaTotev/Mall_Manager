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
        private IAppView m_NextView;
        private readonly Room m_CurrentRoom;
        private readonly StringBuilder m_StringBuilder = new StringBuilder();
        public RoomInfoPage(Room currentRoom)
        {
            InitializeComponent();
            m_CurrentRoom = currentRoom;

            m_StringBuilder.Append(m_CurrentRoom.Name);
            m_StringBuilder.Append(" - Information");
            Tbl_HeaderText.Text = m_StringBuilder.ToString();
            Tb_Name.Text = m_CurrentRoom.Name;
            Tb_Desc.Text = m_CurrentRoom.Description;
            Lb_DateCreated.Content = m_CurrentRoom.CreateDate.ToShortDateString();
            Lb_LastEdited.Content = m_CurrentRoom.LastEditDate.ToShortDateString();

            foreach (var type in RoomManager.GetInstance().GetRoomTypes())
            {
                Cmb_RoomType.Items.Add(type);
            }
            Cmb_RoomType.Text = m_CurrentRoom.Type;
        }

        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeViewForward(m_PreviousView, this);
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

        //TODO Complete Save functionality.
        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            string newName = Tb_Name.Text;
            string newDesc = Tb_Desc.Text;
            string newType = Cmb_RoomType.SelectionBoxItemStringFormat;
            //int newNumber;
            //int newFloor;
            //RoomManager.GetInstance().EditRoom(MallManager.GetInstance().CurrentMall.Name, m_CurrentRoom.Id,Tb_Name.Text,Tb_Desc.Text,Cmb_RoomType.SelectionBoxItemStringFormat);
        }

        private void Tb_Desc_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Tb_Name_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeViewBackward(m_PreviousView, this);
        }
    }
}
