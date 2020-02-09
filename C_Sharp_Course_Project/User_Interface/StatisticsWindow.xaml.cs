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

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        delegate void SetLabelText(Label lbl, string text);
        private BackgroundWorker m_Worker;


        public StatisticsWindow()
        {
            InitializeComponent();
            m_Worker = new BackgroundWorker {WorkerReportsProgress = true};
            m_Worker.DoWork += (worker, args) =>
            {
                //this will be executed on a background worker thread
                if (!args.Cancel)
                {
                    args.Result = DoSomething((int) args.Argument, worker as BackgroundWorker);
                }
            };
            m_Worker.ProgressChanged += (o, args) =>
            {
                //this will be executed on the main UI thread
                Lb_Test.Content = $"Calculating... {args.ProgressPercentage}%";
            };
            m_Worker.RunWorkerCompleted += (o, args) =>
            {
                //this will be executed on the main UI thread
                if (args.Error != null)
                {
                    Lb_Test.Content = $"Error!!! {args.Error.Message}";
                }
                else if (args.Cancelled)
                {
                    Lb_Test.Content = "The operations was canceled";
                }
                else
                {
                    Lb_Test.Content = $"Done! The Value is {args.Result}";
                }
            };
        }

        private void Btn_Load_OnClick(object sender, RoutedEventArgs e)
        {
            //var thing = DoSomething();
            if (!m_Worker.IsBusy)
            {
                Lb_Test.Content = "Calculating...";
                int stargingValue = 5;
                m_Worker.RunWorkerAsync(5);
            }
        }


        

        private int DoSomething(int startingValue, BackgroundWorker worker)
        {
            //this will be called by the background worker on the background worker thread
            int thing = 1;
            decimal total = 1000000000;
            for (int i = 0; i < total; i++)
            {
                thing++;
                if (i != 0 && i % 10000000 == 0)
                {
                    decimal prc = (i / total) * 100M;
                    worker.ReportProgress((int) prc);
                    if (worker.CancellationPending)
                    {
                        break;
                    }
                }
            }

            return thing + startingValue;
        }
    }
}