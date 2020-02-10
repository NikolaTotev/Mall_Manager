using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Core;

namespace User_Interface
{
    class ActivityListItem : INotifyPropertyChanged
    {
        private bool m_IsSelected;
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public Guid ActivityId { get; set; }
        public string Description { get; set; }
        public ActivityStatus Status { get; set; }
        public string Category { get; set; }

        public Brush StatusColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetStatusColor(ActivityStatus status)
        {
            switch (status)
            {
                case ActivityStatus.Scheduled:
                    StatusColor = (Brush)Application.Current.Resources["ScheduledTask"];
                    break;
                case ActivityStatus.InProgress:
                    StatusColor = (Brush)Application.Current.Resources["InProgressTask"];
                    break;
                case ActivityStatus.Finished:
                    StatusColor = (Brush)Application.Current.Resources["Completed"];
                    break;
                case ActivityStatus.Failed:
                    StatusColor = (Brush)Application.Current.Resources["FailedTask"];
                    break;
                case ActivityStatus.Undefined:
                    StatusColor = (Brush)Application.Current.Resources["UndefinedTask"];
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}
