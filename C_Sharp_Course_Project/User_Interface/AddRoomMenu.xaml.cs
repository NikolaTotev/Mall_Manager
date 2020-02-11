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
    /// Interaction logic for AddRoomMenu.xaml
    /// </summary>
    public partial class AddRoomMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private IAppView m_NextView;
        private readonly string m_NameDefaultText = "Room Name";
        private readonly string m_DescDefaultText = "Room Description";
        private readonly string m_TypeDefaultText = "Room Type";
        private readonly string m_NumberDefaultText = "Room Number";
        private readonly string m_FloorDefaultText = "Floor Number";
        private int m_RoomNumber;
        private int m_FloorNumber;
        private string m_RoomType = "";
        public AddRoomMenu()
        {
            InitializeComponent();

            foreach (var type in RoomManager.GetInstance().GetRoomTypes())
            {
                Cmb_RoomType.Items.Add(type);
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

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            RoomManager.GetInstance().CreateRoom(Tb_RoomName.Text, Tb_RoomDesc.Text, m_RoomType,
                m_FloorNumber, m_RoomNumber, Guid.NewGuid(),
                MallManager.GetInstance().CurrentMall.Name);
            RoomsMenu menu = new RoomsMenu();
            m_CurrentMainWindow.ReturnFromAddMenu(menu,m_PreviousView.GetPreviousView());
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ReturnFromAddMenu(m_PreviousView, m_PreviousView);
        }

        #region  Room name text box functions 
        private void Tb_RoomName_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_RoomName.Text == m_NameDefaultText)
            {
                Tb_RoomName.Text = "";
            }
        }

        private void Tb_RoomName_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_RoomName.Text))
            {
                Tb_RoomName.Text = m_NameDefaultText;
            }
        }

        private void Tb_RoomName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        #endregion

        #region Room desc text box functions
        private void Tb_RoomDesc_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_RoomDesc.Text == m_DescDefaultText)
            {
                Tb_RoomDesc.Text = "";
            }
        }

        private void Tb_RoomDesc_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_RoomDesc.Text))
            {
                Tb_RoomDesc.Text = m_DescDefaultText;
            }
        }

        private void Tb_RoomDesc_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }
        #endregion

        #region Room number text box functions
        private void Tb_RoomNumber_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_RoomNumber.Text == m_NumberDefaultText)
            {
                Tb_RoomNumber.Text = "";
            }
        }

        private void Tb_RoomNumber_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_RoomNumber.Text))
            {
                Tb_RoomNumber.Text = m_NumberDefaultText;
            }
        }

        private void Tb_RoomNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }
        #endregion

        #region Floor number text box functions
        private void Tb_FloorNumber_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_FloorNumber.Text == m_FloorDefaultText)
            {
                Tb_FloorNumber.Text = "";
            }
        }

        private void Tb_FloorNumber_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_FloorNumber.Text))
            {
                Tb_FloorNumber.Text = m_FloorDefaultText;
            }
        }

        private void Tb_FloorNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }
        #endregion

        private void Validate()
        {
            if (Tb_RoomName != null && Tb_RoomDesc != null && Tb_RoomNumber != null && Tb_FloorNumber != null && Btn_Add != null)
            {
                bool nameOk = Tb_RoomName.Text != m_NameDefaultText && !string.IsNullOrEmpty(Tb_RoomName.Text);
                bool descOk = Tb_RoomDesc.Text != m_DescDefaultText && !string.IsNullOrEmpty(Tb_RoomDesc.Text);
                bool typeOk = !string.IsNullOrEmpty(m_RoomType);
                bool numberOk = Tb_RoomNumber.Text != m_NumberDefaultText && int.TryParse(Tb_RoomNumber.Text, out m_RoomNumber);
                bool floorOk = Tb_FloorNumber.Text != m_FloorDefaultText && int.TryParse(Tb_FloorNumber.Text, out m_FloorNumber);

                if (nameOk && descOk && typeOk && numberOk && floorOk)
                {
                    Btn_Add.IsEnabled = true;
                    Lb_NameError.Content = "";
                    Lb_DescError.Content = "";
                    Lb_RoomTypeError.Content = "";
                    Lb_RoomNumberError.Content = "";
                    Lb_FloorNumberError.Content = "";
                }
                else
                {
                    Btn_Add.IsEnabled = false;
                    Lb_NameError.Content = !nameOk ? m_CurrentMainWindow.Strings["RoomNameInvalid"] : "";
                    Lb_DescError.Content = !descOk ? m_CurrentMainWindow.Strings["RoomDescInvalid"] : "";
                    Lb_RoomTypeError.Content = !typeOk ? m_CurrentMainWindow.Strings["RoomTypeInvalid"] : "";
                    Lb_RoomNumberError.Content = !numberOk ? m_CurrentMainWindow.Strings["MustBeANumberError"] : "";
                    Lb_FloorNumberError.Content = !floorOk ? m_CurrentMainWindow.Strings["MustBeANumberError"] : "";
                }
            }
        }

        private void Cmb_RoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_RoomType = Cmb_RoomType.SelectedItem.ToString();
        }
    }
}
