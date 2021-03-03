using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Model;

namespace WpfApp4
{
    public class FirefightersViewModel
    {
        private ObservableCollection<Firefighter> firefighters = new ObservableCollection<Firefighter>();
        public ObservableCollection<Firefighter> FirefighterList
        {
            get { return firefighters; }
            set
            {
                if (firefighters != value)
                    firefighters = value;
            }
        }

        public FirefightersViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void copyOrDefault(object existingFirefighter)
        {
            var ff = new Firefighter();
            if (null != existingFirefighter)
            {
                var selectedFF = existingFirefighter as Firefighter;
                ff.Car = selectedFF.Car;
                ff.setTimes(selectedFF.DispatchTime, selectedFF.ReturnTime);
            }
            firefighters.Add(ff);
        }
        public bool calcTimeSpent(bool validate = false)
        {
            foreach (Firefighter ff in firefighters)
            {
                if (validate)
                {
                    if (!ff.isFilled())
                        return false;
                }
                else
                    ff.calcTime();
            }
            return true;
        }
        public void remove(object selectedRow)
        {
            var item = selectedRow as Firefighter;
            if (null != item)
                firefighters.Remove(item);
        }
    }
}

