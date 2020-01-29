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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        public Dashboard()
        {
            InitializeComponent();
            LoadMallList();
        }

        public void LoadMallList()
        {
            foreach (string mall in MallManager.GetInstance().GetMalls().Values.ToList())
            {
                string mallName = mall.Split()[0];
                BigButton newBigButton = new BigButton("pack://application:,,,/Resources/Icons/StoreFront_Icon.png", mallName);
                Lv_Malls.Items.Add(newBigButton);
            }
        }

        private void OnClick_Add(object sender, RoutedEventArgs e)
        {
            AddMallMenu addMall = new AddMallMenu();
            m_CurrentMainWindow.ChangeView(addMall,this);
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }
    }
}
