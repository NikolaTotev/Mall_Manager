using System.Windows;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for Mall.xaml
    /// </summary>
    public partial class MallMenu : IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private SettingsWindow m_SettingsWindow = new SettingsWindow();

        public MallMenu()
        {
            InitializeComponent();
            Tbl_HeaderText.Text = MallManager.GetInstance().CurrentMall.Name;
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

        private void Btn_RentalSpaces_Click(object sender, RoutedEventArgs e)
        {
            RoomsMenu rooms = new RoomsMenu();
            m_SettingsWindow?.Close();
            m_CurrentMainWindow.ChangeViewForward(rooms, this);
        }

        private void Btn_Statistics_OnClick(object sender, RoutedEventArgs e)
        {
            m_SettingsWindow?.Close();
        }

        private void Btn_ActivityOverview_OnClick(object sender, RoutedEventArgs e)
        {
            MallActivities mallActivities = new MallActivities();

            m_SettingsWindow?.Close();

            m_CurrentMainWindow.ChangeViewForward(mallActivities, this);
        }

        private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
        {
            m_SettingsWindow?.Close();
            m_CurrentMainWindow.ChangeViewBackward(m_PreviousView, this);
        }
        private void Btn_Settings_OnClick(object sender, RoutedEventArgs e)
        {
            m_SettingsWindow = new SettingsWindow();
            m_SettingsWindow.Show();
        }
    }
}
