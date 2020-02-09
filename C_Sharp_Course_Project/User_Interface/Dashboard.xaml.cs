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
           // this.Loaded += (sender, args) => ActivityManager.GetInstance().ActivityAdded += OnActivityAdded;
           // this.Unloaded += (sender, args) => ActivityManager.GetInstance().ActivityAdded -= OnActivityAdded;
        }

        private void OnActivityAdded(object sender, EventArgs e)
        {
          //  throw new NotImplementedException();
        }


        public void LoadMallList()
        {
            if (MallManager.GetInstance().Malls.Count == 2)
            {
                Btn_Add.IsEnabled = false;
                Btn_Add.Opacity = 0.5;

            }

            foreach (var mall in MallManager.GetInstance().GetMalls())
            {
                string mallName = mall.Value.Split()[0];
                BigButton newBigButton = new BigButton("pack://application:,,,/Resources/Icons/StoreFront_Icon.png", mallName);
                newBigButton.Tag = mall.Key;
                AddHandler(BigButton.ClickedEvent, new RoutedEventHandler(OnMallChanged));
                Sp_MallBtns.Children.Add(newBigButton);
            }
        }

        private void OnMallChanged(object sender, RoutedEventArgs args)
        {
            if (args.OriginalSource is BigButton button)
            {
                MallManager.GetInstance().ChangeCurrentMall((Guid)button.Tag);
                MallMenu mallMenu = new MallMenu();
                m_CurrentMainWindow.ChangeView(mallMenu, this);
            }
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
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
