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
            WorkerThreadInitialization();
            m_RoomId = roomId;
            this.Loaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged += OnActivityInfoChanged;
            this.Unloaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged -= OnActivityInfoChanged;
            GraphInitialization();
        }

        private void GraphInitialization()
        {
            BackgroundWorker initWorker = new BackgroundWorker { WorkerReportsProgress = true };

            initWorker.DoWork += (worker, args) =>
            {
                if (!args.Cancel) args.Result = VisualizationPreProcessor.GetRoomActivityInfoData(m_RoomId);
            };

            initWorker.ProgressChanged += (o, args) => { };
            initWorker.RunWorkerCompleted += (o, args) =>
             {
                 if (args.Error != null)
                 {

                 }
                 else if (args.Cancelled)
                 {

                 }
                 else
                 {
                     m_currentGraph = new SimpleBarGraph(VisualizationPreProcessor.GenerateBasicActivityColumnGraphics(args.Result as IList<(string Title, int Value, ActivityStatus Status)>));
                     Thickness margin = m_currentGraph.Margin;
                     double margins = 20;
                     margin.Top = margins;
                     margin.Bottom = margins;
                     margin.Left = margins;
                     margin.Right = margins;
                     m_currentGraph.Margin = margin;
                     G_MainGrid.Children.Clear();
                     G_MainGrid.Children.Add(m_currentGraph);
                 }
             };

            if (!initWorker.IsBusy)
            {
                initWorker.RunWorkerAsync();
            }
        }

        private void WorkerThreadInitialization()
        {
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
                    m_currentGraph.UpdateData(VisualizationPreProcessor.GenerateBasicActivityColumnGraphics(args.Result as IList<(string Title, int Value, ActivityStatus Status)>));
                }
            };
        }


        private void OnActivityInfoChanged(object sender, EventArgs e)
        {
            if (!m_Worker.IsBusy)
            {
                ///Testing code used in demo will be removed later do not touch.
                //Lb_Test.Content = "Calculating...";
                m_Worker.RunWorkerAsync(5);
            }
        }

        /// <summary>
        /// WIP - DO NOT USE
        /// </summary>
        /// <param name="current"></param>
        /// <param name="interval"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private decimal PercentageCalculator(decimal current, decimal interval, decimal total)
        {
            if (current != 0 && current % interval == 0)
            {
                return (current / total) * 100M;
            }
            return 0;
            /*
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
           //}*/
        }

        private IList<(string Title, int Value, ActivityStatus Status)> ProcessCurrentData(int startingValue, BackgroundWorker worker)
        {
            return VisualizationPreProcessor.GetRoomActivityInfoData(m_RoomId);
        }
    }
}