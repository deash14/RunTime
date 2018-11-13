using System;
using System.Collections.Generic;

namespace Model
{
    public class Data
    {
        [Flags]
        public enum DistanceTypes
        {
            miles = 0,
            kilometers
        }

        [Flags]
        public enum PaceTypes
        {
            minutesPerMile = 0,
            minutesPerKilometer
        }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public double Seconds { get; set; }
        public int PaceHours { get; set; }
        public int PaceMinutes { get; set; }
        public double PaceSeconds { get; set; }
        public double Distance { get; set; }
        public double SplitDistance { get; set; }
        public Boolean CalcPace { get; set; }
        public Boolean CalcDist { get; set; }
        public Boolean CalcTime { get; set; }
        public DistanceTypes DistanceType { get; set; }
        public DistanceTypes SplitDistanceType { get; set; }
        public PaceTypes PaceType { get; set; }
        public List<Split> SplitList { get; set; }

        /// <summary>
        ///  default, empty constructor
        /// </summary>
        public Data()
        {
            // set some default types
            DistanceType = DistanceTypes.kilometers;
            SplitDistanceType = DistanceTypes.kilometers;
            PaceType = PaceTypes.minutesPerKilometer;

            // initialize the Split list
            SplitList = new List<Split>();
        }

        /// <summary>
        /// Determines if any of the base data is not valid.
        /// </summary>
        /// <returns></returns>
        public Boolean ValidBaseData()
        {
            Boolean isValid = false;

            if (Hours >= 0 &&
                Minutes >= 0 &&
                Seconds >= 0 &&
                PaceHours >= 0 &&
                PaceMinutes >= 0 &&
                PaceSeconds >= 0 &&
                Distance >= 0)
            {
                isValid = true;
            }

            return isValid;
        }

        public Boolean ValidExtendedData()
        {
            Boolean isValid = false;

            if (SplitDistance > 0 &&
                ValidBaseData())
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
