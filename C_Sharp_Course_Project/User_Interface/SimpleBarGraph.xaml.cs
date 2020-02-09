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
using LiveCharts;
using LiveCharts.Wpf;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for SimpleBarGraph.xaml
    /// </summary>
    public partial class SimpleBarGraph : UserControl
    {
        public SimpleBarGraph(SeriesCollection activityData)
        {
            InitializeComponent();

            SeriesCollection = activityData;
            Formatter = value => value.ToString("N");
            
            DataContext = this;
        }


        public void UpdateData(SeriesCollection collection)
        {
            SeriesCollection.Clear();
            foreach (var item in collection)
            {
                SeriesCollection.Add(item);
            }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}