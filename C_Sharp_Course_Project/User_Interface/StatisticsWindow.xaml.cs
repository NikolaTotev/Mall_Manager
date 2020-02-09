using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Core;
using LiveCharts;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        delegate void SetLabelText(Label lbl, string text);
        private BackgroundWorker m_Worker;
        private Guid m_RoomId;
        private SimpleBarGraph m_currentGraph;
        public StatisticsWindow(Guid roomId)
        {

            InitializeComponent();
            m_RoomId = roomId;
            m_currentGraph = new SimpleBarGraph(VisualizationPreProcessor.BasicActivityInfo(m_RoomId));
            G_MainGrid.Children.Add(m_currentGraph);

            this.Loaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged += OnActivityAdded;
            this.Unloaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged -= OnActivityAdded;

            m_Worker = new BackgroundWorker { WorkerReportsProgress = true };
            m_Worker.DoWork += (worker, args) =>
            {
                //this will be executed on a background worker thread
                if (!args.Cancel)
                {
                    args.Result = ProcessCurrentData((int)args.Argument, worker as BackgroundWorker);
                }
            };
            m_Worker.ProgressChanged += (o, args) =>
            {
                ///Testing code used in demo will be removed later do not touch.
                //this will be executed on the main UI thread
                // Lb_Test.Content = $"Calculating... {args.ProgressPercentage}%";
            };
            m_Worker.RunWorkerCompleted += (o, args) =>
            {
                //this will be executed on the main UI thread
                if (args.Error != null)
                {
                    ///Testing code used in demo will be removed later do not touch.
                    //Lb_Test.Content = $"Error!!! {args.Error.Message}";
                }
                else if (args.Cancelled)
                {
                    ///Testing code used in demo will be removed later do not touch.
                    //Lb_Test.Content = "The operations was canceled";
                }
                else
                {
                    m_currentGraph.UpdateData(args.Result as SeriesCollection);
                }
            };
        }

       
        private void OnActivityAdded(object sender, EventArgs e)
        {
            if (!m_Worker.IsBusy)
            {
                ///Testing code used in demo will be removed later do not touch.
                //Lb_Test.Content = "Calculating...";
                m_Worker.RunWorkerAsync(5);
            }
        }

        private void Btn_Load_OnClick(object sender, RoutedEventArgs e)
        {
            if (!m_Worker.IsBusy)
            {
                ///Testing code used in demo will be removed later do not touch.
                //Lb_Test.Content = "Calculating...";
                m_Worker.RunWorkerAsync(5);
            }
        }

        private SeriesCollection ProcessCurrentData(int startingValue, BackgroundWorker worker)
        {
            ///Testing code used in demo will be removed later do not touch.
            //int thing = 1;
            //decimal total = 1000000000;
            //for (int i = 0; i < total; i++)
            //{
            //    thing++;
            //    if (i != 0 && i % 10000000 == 0)
            //    {
            //        decimal prc = (i / total) * 100M;
            //        worker.ReportProgress((int)prc);
            //        if (worker.CancellationPending)
            //        {
            //            break;
            //        }
            //    }
            //}
            return VisualizationPreProcessor.BasicActivityInfo(m_RoomId);
        }
    }
}