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
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for HeatMap.xaml
    /// </summary>
    public partial class HeatMap : UserControl
    {
        public HeatMap((List<string>, ChartValues<HeatPoint>)? data)
        {
            InitializeComponent();
            Random r = new Random();

            Values = new ChartValues<HeatPoint>();
            Rooms = new List<string>();

            Statuses = new[]
            {
                "Scheduled",
                "In Progress",
                "Completed",
                "Failed"
            };
            UpdateData(data);
            DataContext = this;
        }

        public ChartValues<HeatPoint> Values { get; private set; }
        public List<string> Rooms { get; private set; }
        public string[] Statuses { get; private set; }

        public void UpdateData((List<string> rooms, ChartValues<HeatPoint> values)? data)
        {
            if (data == null) return;
            Rooms.Clear();
            Values.Clear();
            foreach (var room in data.Value.rooms)
            {
                Rooms.Add(room);
            }
            foreach (var point in data.Value.values)
            {
                Values.Add(point);
            }

        }
    }
}
