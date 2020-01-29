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
        private Guid m_MallId;
        public BigButton(string imageSource, string buttonText, Guid mallId)
        {
            InitializeComponent();
            m_MallId = mallId;
            Tb_ButtonName.Text = buttonText;
            Img_ButtonImage.Source = new BitmapImage(new Uri(imageSource));
        }

    }
}
