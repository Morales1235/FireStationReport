using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireReportMain.Model
{
    public class Firefighter : INotifyPropertyChanged
    {
        private String name;
        private String car;
        private DateTime dispatchTime;
        private DateTime returnTime;
        private TimeSpan timeSpent;

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public String Car
        {
            get
            {
                return car;
            }
            set
            {
                car = value;
                OnPropertyChanged("Car");
            }
        }
        public String DispatchTime
        {
            get
            {
                return dispatchTime.ToString(Glob.Global.datetimeTimeonlyFormat);
            }
            set
            {
                DateTime.TryParseExact(value, Glob.Global.datetimeTimeonlyFormat, null, System.Globalization.DateTimeStyles.None, out dispatchTime);
                OnPropertyChanged("DispatchTime");
            }
        }
        public String ReturnTime
        {
            get
            {
                return returnTime.ToString(Glob.Global.datetimeTimeonlyFormat);
            }
            set
            {
                DateTime.TryParseExact(value, Glob.Global.datetimeTimeonlyFormat, null, System.Globalization.DateTimeStyles.None, out returnTime);
                OnPropertyChanged("ReturnTime");
            }
        }
        public int MinutesSpent
        {
            get
            {
                return (int)timeSpent.TotalMinutes;
            }
        }
        public void setTimes(DateTime dispatch, DateTime ret)
        {
            dispatchTime = dispatch;
            returnTime = ret;
            OnPropertyChanged("DispatchTime");
            OnPropertyChanged("ReturnTime");
            calcTime();
        }
        public void setTimes(string dispatch, string ret)
        {
            dispatchTime = DateTime.ParseExact(dispatch, Glob.Global.datetimeTimeonlyFormat, null); ;
            returnTime = DateTime.ParseExact(ret, Glob.Global.datetimeTimeonlyFormat, null);
            OnPropertyChanged("DispatchTime");
            OnPropertyChanged("ReturnTime");
            calcTime();
        }
        public void calcTime()
        {
            timeSpent = returnTime - dispatchTime;
            if (timeSpent.Minutes < 0 || timeSpent.Hours < 0)
                 timeSpent += TimeSpan.FromHours(24);
        }

        public bool isFilled()
        {
            if (name == null || name == String.Empty)
                return false;
            if (Car == null || car == String.Empty)
                return false;
            if (dispatchTime == null || dispatchTime == DateTime.MinValue)
                return false;
            if (returnTime == null || returnTime == DateTime.MinValue)
                return false;
            calcTime();
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
