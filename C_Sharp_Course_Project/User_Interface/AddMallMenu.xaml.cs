using System;
using System.Windows;
using System.Windows.Controls;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for AddMallMenu.xaml
    /// </summary>
    public partial class AddMallMenu : IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;

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
            //Implement as needed
        }

        public IAppView GetPreviousView()
        {
            return m_PreviousView;
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {

            m_CurrentMainWindow.ReturnFromAddMenu(m_PreviousView,m_PreviousView.GetPreviousView());
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            Mall mallToAdd = new Mall(Guid.NewGuid(), Tb_MallName.Text, Tb_MallDesc.Text);
            MallManager.GetInstance().AddMall(mallToAdd);
            Dashboard newDash = new Dashboard();
            newDash.SetPreviousView(this);
            newDash.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ReturnFromAddMenu(newDash,m_PreviousView.GetPreviousView());
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
