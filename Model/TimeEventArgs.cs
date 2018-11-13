using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TimeEventArgs : EventArgs
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public double Seconds { get; private set; }

        public TimeEventArgs(int hours, int minutes, double seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}
