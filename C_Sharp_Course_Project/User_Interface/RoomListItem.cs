using System;
using System.ComponentModel;
using System.Windows.Media;
using Core;

namespace User_Interface
{
    class RoomListItem : INotifyPropertyChanged
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

        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public string RoomNumber { get; set; }
        public string RoomFloor{ get; set; }
        public string DateCreated { get; set; }
        public string LastEdited { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}