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
        private IAppView m_PreviousElement;
        private readonly string m_NameDefaultText = "Room Name";
        private readonly string m_DescDefaultText = "Room Description";
        private readonly string m_TypeDefaultText = "Room Type";
        private readonly string m_NumberDefaultText = "Room Number";
        private readonly string m_FloorDefaultText = "Floor Number";
        private int m_RoomNumber;
        private int m_FloorNumber;
        public AddRoomMenu()
        {
            InitializeComponent();
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousElement = previousElement;
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            RoomManager.GetInstance().CreateRoom(Tb_RoomName.Text, Tb_RoomDesc.Text, Tb_RoomType.Text,
                m_FloorNumber, m_RoomNumber, Guid.NewGuid(),
                MallManager.GetInstance().CurrentMall.Name);
            RoomsMenu menu = new RoomsMenu();
            m_CurrentMainWindow.ChangeView(menu, this);
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeView(m_PreviousElement, this);
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

        #region Room type text box functions
        private void Tb_RoomType_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_RoomType.Text == m_TypeDefaultText)
            {
                Tb_RoomType.Text = "";
            }
        }

        private void Tb_RoomType_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_RoomType.Text))
            {
                Tb_RoomType.Text = m_TypeDefaultText;
            }
        }

        private void Tb_RoomType_OnTextChanged(object sender, TextChangedEventArgs e)
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
            if (Tb_RoomName != null && Tb_RoomDesc != null && Tb_RoomType != null && Tb_RoomNumber != null && Tb_FloorNumber != null && Btn_Add != null)
            {
                bool nameOk = Tb_RoomName.Text != m_NameDefaultText && !string.IsNullOrEmpty(Tb_RoomName.Text);
                bool descOk = Tb_RoomDesc.Text != m_DescDefaultText && !string.IsNullOrEmpty(Tb_RoomDesc.Text);
                bool typeOk = Tb_RoomType.Text != m_TypeDefaultText && !string.IsNullOrEmpty(Tb_RoomType.Text);
                bool numberOk = Tb_RoomNumber.Text != m_NumberDefaultText && int.TryParse(Tb_RoomNumber.Text, out m_RoomNumber);
                bool floorOk = Tb_FloorNumber.Text != m_FloorDefaultText && int.TryParse(Tb_FloorNumber.Text, out m_FloorNumber);

                if (nameOk && descOk && typeOk && numberOk && floorOk)
                {
                    Btn_Add.IsEnabled = true;
                }
                else
                {
                    Btn_Add.IsEnabled = false;

                    if (!nameOk)
                    {
                        Lb_NameError.Content = m_CurrentMainWindow.Strings["RoomNameInvalid"];
                    }
                    else
                    {
                        Lb_NameError.Content = "";
                    }

                    if (!descOk)
                    {
                        Lb_DescError.Content = m_CurrentMainWindow.Strings["RoomDescInvalid"];
                    }
                    else
                    {
                        Lb_DescError.Content = "";
                    }

                    if (!typeOk)
                    {
                        Lb_TypeError.Content = m_CurrentMainWindow.Strings["RoomTypeInvalid"];
                    }
                    else
                    {
                        Lb_TypeError.Content = "";
                    }

                    if (!numberOk)
                    {
                        Lb_RoomNumberError.Content = m_CurrentMainWindow.Strings["MustBeANumberError"];
                    }
                    else
                    {
                        Lb_RoomNumberError.Content = "";
                    }

                    if (!floorOk)
                    {
                        Lb_FloorNumberError.Content = m_CurrentMainWindow.Strings["MustBeANumberError"];
                    }
                    else
                    {
                        Lb_FloorNumberError.Content = "";
                    }
                }
            }
        }
    }
}
