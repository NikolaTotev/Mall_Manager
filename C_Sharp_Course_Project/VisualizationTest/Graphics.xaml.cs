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

namespace VisualizationTest
{
    /// <summary>
    /// Interaction logic for Graphics.xaml
    /// </summary>
    public partial class Graphics : UserControl
    {
        public Graphics()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new StackedRowSeries()
                {
                    Values = new ChartValues<double> {10},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedRowSeries
                {
                    Values = new ChartValues<double> {1},
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedRowSeries
                {
                    Values = new ChartValues<double> {1},
                    StackMode = StackMode.Values
                }
            };

            //adding series updates and animates the chart
            SeriesCollection.Add(new StackedRowSeries
            {
                Values = new ChartValues<double> { 6 },
                StackMode = StackMode.Values
            });

            //adding values also updates and animates
            //SeriesCollection[0].Values.Add(4d);

            Labels = new[] { "Chrome" };
            //Formatter = value => value + " Mill";

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}

