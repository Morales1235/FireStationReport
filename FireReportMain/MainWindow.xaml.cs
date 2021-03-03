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
using Glob;
using Xceed.Wpf.Toolkit;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FireReportMain.PDF;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace FireReportMain
{
    public partial class MainWindow : Window
    {
        FirefightersViewModel firefighters = new FirefightersViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = firefighters;
            numberTextBox.Focus();
        }

        private void memberListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            addRowButton_Click(sender, (RoutedEventArgs)e);
        }

        private void addRowButton_Click(object sender, RoutedEventArgs e)
        {
            object selectedFF = null;
            if (this.memberListView.Items.Count > 0)
            {
                selectedFF = this.memberListView.SelectedItem;
                if (null == selectedFF)
                    selectedFF = this.memberListView.Items[this.memberListView.Items.Count - 1];
            }
            firefighters.copyOrDefault(selectedFF);
            focusOnLastElement();
        }

        private void focusOnLastElement()
        {
            memberListView.SelectedIndex = memberListView.Items.Count - 1;
            ListViewItem it = memberListView.ItemContainerGenerator.ContainerFromIndex(memberListView.SelectedIndex) as ListViewItem;
            if (null != it)
                it.Focus();
        }

        private void timeTextBoxGotFocus(object sender, RoutedEventArgs args)
        {
            if(sender is MaskedTextBox)
            {
                var txtBox = (MaskedTextBox)sender;
                if(!txtBox.IsMaskCompleted || txtBox.Text == "00:00")
                {
                    txtBox.Clear();
                    txtBox.Dispatcher.BeginInvoke(new Action(() => txtBox.CaretIndex = 0), System.Windows.Threading.DispatcherPriority.Background);
                }
            }
        }
        private void timeTextBoxLostFocus(object sender, RoutedEventArgs args)
        {
        }
        private void numberTextBoxLostFocus(object sender, RoutedEventArgs args)
        {
            if (sender is MaskedTextBox)
            {
                if (memberListView.Items.Count == 0)
                    addRowButton_Click(sender, args);
            }
        }

        private void numberTextBoxGotFocus(object sender, RoutedEventArgs args)
        {
            if(sender is MaskedTextBox)
            {
                var txtBox = sender as MaskedTextBox;
                if(!txtBox.IsMaskCompleted)
                {
                    txtBox.Clear();
                    txtBox.Dispatcher.BeginInvoke(new Action(() => txtBox.CaretIndex = 0), System.Windows.Threading.DispatcherPriority.Background);
                }
            }
        }

        private void generateReportButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (firefighters.calcTimeSpent(true) &&
                getGlobalData(ref data))
            {
                var pdfGen = new SalaryReport(data, this.firefighters);
                pdfGen.openCreated();
            }
            else
            {
                MessageBox.Show("Nie wszystkie dane są poprawnie uzupełnione.");
            }
        }

        private bool getGlobalData(ref Dictionary<string, string> data)
        {
            data["number"] = numberTextBox.Text;
            if (true == todayDateCheckBox.IsChecked)
                data["date"] = dateTextBox.Text;
            else
                data["date"] = DateTime.Today.ToString(Glob.Global.datetimeDateonlyFormat);
            data["description"] = descriptionTextBox.Text;
            if (data.Any(v => (v.Value == null || v.Value == String.Empty)))
                return false;
            return true;
        }

        private void FocusTextBoxOnLoad(object sender, RoutedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if (null == txtBox)
                return;
            txtBox.Focus();
        }

        private void removeMemberButton_Click(object sender, RoutedEventArgs e)
        {
            firefighters.remove(memberListView.SelectedItem);
        }

        private void todayDateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            dateTextBox.IsEnabled = true;
        }
    }
}
