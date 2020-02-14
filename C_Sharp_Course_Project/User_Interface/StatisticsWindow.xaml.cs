using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow
    {
        private BackgroundWorker m_Worker;
        private readonly bool m_CallingFromMall;
        private readonly Guid m_RoomId;
        private SimpleBarGraph m_CurrentGraph;
        public StatisticsWindow(Guid roomId, bool callingFromMall)
        {

            InitializeComponent();
            WorkerThreadInitialization();
            m_RoomId = roomId;
            m_CallingFromMall = callingFromMall;
            Loaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged += OnActivityInfoChanged;
            Unloaded += (sender, args) => ActivityManager.GetInstance().ActivitiesChanged -= OnActivityInfoChanged;
            GraphInitialization();
        }

        private void GraphInitialization()
        {
            BackgroundWorker initWorker = new BackgroundWorker { WorkerReportsProgress = true };

            initWorker.DoWork += (worker, args) =>
            {
                if (m_CallingFromMall)
                {
                    if (!args.Cancel) args.Result = VisualizationPreProcessor.GetMallAssociatedActivityInfoData(m_RoomId);
                }
                else
                {
                    if (!args.Cancel) args.Result = VisualizationPreProcessor.GetRoomActivityInfoData(m_RoomId);
                }
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
                     m_CurrentGraph = new SimpleBarGraph(VisualizationPreProcessor.GenerateBasicActivityColumnGraphics(args.Result as IList<(string Title, List<int> Value, ActivityStatus Status)>));
                     Thickness margin = m_CurrentGraph.Margin;
                     double margins = 20;
                     margin.Top = margins;
                     margin.Bottom = margins;
                     margin.Left = margins;
                     margin.Right = margins;
                     m_CurrentGraph.Margin = margin;
                     G_MainGrid.Children.Clear();
                     G_MainGrid.Children.Add(m_CurrentGraph);
                 }
             };

            if (!initWorker.IsBusy)
            {
                initWorker.RunWorkerAsync();
            }
        }

        private void WorkerThreadInitialization()
        {
            m_Worker = new BackgroundWorker { WorkerReportsProgress = true};
            m_Worker.DoWork += (worker, args) =>
            {
                //this will be executed on a background worker thread
                if (m_CallingFromMall)
                {
                    if (!args.Cancel) args.Result = VisualizationPreProcessor.GetMallAssociatedActivityInfoData(m_RoomId);
                }
                else
                {
                    if (!args.Cancel) args.Result = VisualizationPreProcessor.GetRoomActivityInfoData(m_RoomId);
                }
            };
            m_Worker.ProgressChanged += (o, args) =>
            {
         
            };
            m_Worker.RunWorkerCompleted += (o, args) =>
            {
                //this will be executed on the main UI thread
                if (args.Error != null)
                {
         
                }
                else if (args.Cancelled)
                {
         
                }
                else
                {
                    m_CurrentGraph.UpdateData(VisualizationPreProcessor.GenerateBasicActivityColumnGraphics(args.Result as IList<(string Title, List<int> Value, ActivityStatus Status)>));
                }
            };
        }


        private void OnActivityInfoChanged(object sender, EventArgs e)
        {
            if (!m_Worker.IsBusy)
            {
                m_Worker.RunWorkerAsync(5);
            }
        }
    }
}