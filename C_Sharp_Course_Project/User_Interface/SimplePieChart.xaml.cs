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
    /// Interaction logic for SimplePieChart.xaml
    /// </summary>
    public partial class SimplePieChart : UserControl
    {
        public Func<ChartPoint, string> PointLabel { get; set; }
        public SimplePieChart()
        {
            InitializeComponent();
            PointLabel = chartPoint =>
                $"{chartPoint.Y} ({chartPoint.Participation:P})";
            DataContext = this;
        }

        public void UpdateData(SeriesCollection collection)
        {
            chart.Series.Clear();
            foreach (var item in collection)
            {
                chart.Series.Add(item);
            }
        }
    }
}
