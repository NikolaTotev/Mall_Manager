using System;
using System.Windows;
using Core;
 
namespace User_Interface
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;

        public Dashboard()
        {
            InitializeComponent();
            LoadMallList();
           // this.Loaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged += OnActivitiesChanged;
           // this.Unloaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged -= OnActivitiesChanged;
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
                BigButton newBigButton =
                    new BigButton("pack://application:,,,/Resources/Icons/StoreFront_Icon.png", mallName)
                    {
                        Tag = mall.Key
                    };
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
                m_CurrentMainWindow.ChangeViewForward(mallMenu, this);
            }
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddMallMenu addMall = new AddMallMenu();
            m_CurrentMainWindow.ChangeViewForward(addMall,this);
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
    }
}
