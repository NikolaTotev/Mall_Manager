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
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for MallActivities.xaml
    /// </summary>
    public partial class MallActivities : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        public MallActivities()
        {
            InitializeComponent();
            Lv_Activities.SelectionMode = SelectionMode.Multiple;

            foreach (var activity in ActivityManager.GetInstance().Activities)
            {
                ListViewItem itemToAdd = new ListViewItem
                {
                    Tag = activity.Key,
                    Content = activity.Value.Description,
                    Name = activity.Value.CurActivityStatus.ToString()
                };
                Lv_Activities.Items.Add(itemToAdd);
            }
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //AddActivityMenu menuToDisplay = new AddActivityMenu();
            //m_CurrentMainWindow.ChangeView(menuToDisplay, this);
        }

        private void Btn_Stats_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem itemToDelete = (ListViewItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().DeleteActivity((Guid)itemToDelete.Tag, MallManager.GetInstance().CurrentMall.Name);
                Lv_Activities.Items.Remove(itemToDelete);
            }
        }

        private void Btn_MarkAsFailed_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = (ListViewItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().Activities[(Guid)item.Tag].CurActivityStatus =
                    ActivityStatus.Failed;  //Should change to CurrentMall.Name
            }
        }

        private void Btn_MarkAsCompleted_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = (ListViewItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().Activities[(Guid)item.Tag].CurActivityStatus =
                    ActivityStatus.Finished;  //Should change to CurrentMall.Name
            }
        }

        private void Btn_Deselect_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_MarkAsInProgress_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = (ListViewItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().Activities[(Guid)item.Tag].CurActivityStatus =
                    ActivityStatus.InProgress;  //Should change to CurrentMall.Name
            }
        }

        private void Btn_MarkAsScheduled_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = (ListViewItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().Activities[(Guid)item.Tag].CurActivityStatus =
                    ActivityStatus.Scheduled;  //Should change to CurrentMall.Name
            }
        }

        private void Btn_SelectAll_OnClick(object sender, RoutedEventArgs e)
        {
            Lv_Activities.SelectAll();
            //It selects every item but they are not marked in the UI
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }
    }
}
