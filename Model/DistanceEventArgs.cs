using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DistanceEventArgs : EventArgs
    {
        public double Distance { get; private set; }

        public DistanceEventArgs(double distance)
        {
            Distance = distance;
        }
    }
}
