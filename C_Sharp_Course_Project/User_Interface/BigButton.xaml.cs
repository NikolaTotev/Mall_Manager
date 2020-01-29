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

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BigButton : UserControl
    {
        private string ImagePath;
        private string ButtonText;
        public BigButton(string imageSource, string buttonText)
        {
            InitializeComponent();
            ImagePath = imageSource;
            ButtonText = buttonText;

            Tb_ButtonName.Text = ButtonText;
            Img_ButtonImage.Source = new BitmapImage(new Uri(ImagePath)); 
        }
    }
}
