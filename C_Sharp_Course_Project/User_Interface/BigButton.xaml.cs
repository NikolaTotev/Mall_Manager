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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BigButton : UserControl
    {
        public static readonly RoutedEvent ClickedEvent = EventManager.RegisterRoutedEvent("ClickedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BigButton));
        // Provide CLR accessors for the event
        
        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickedEvent, value);
            remove => RemoveHandler(ClickedEvent, value);
        }
        //public delegate bool ChangeMall (Guid mallId);
        //ChangeMall handler;
        public BigButton(string imageSource, string buttonText)
        {
            InitializeComponent();
            Tb_ButtonName.Text = buttonText;
            Img_ButtonImage.Source = new BitmapImage(new Uri(imageSource));
        }

        void RaiseClickedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ClickedEvent) {Source = this};
            RaiseEvent(newEventArgs);
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickedEvent();
        }
    }
}
