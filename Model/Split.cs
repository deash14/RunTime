using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Split
    {
        public double Distance { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public double Seconds { get; set; }

        public Split(int hours, int minutes, double seconds, double distance)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Distance = distance;
        }
    }
}
