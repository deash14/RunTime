using System;

namespace Model
{
    /// <summary>
    /// This class calculates all possible data that is generated.
    /// In the case of a mismatch of distance types and pace types, e.g. miles with minutes/kilometer, 
    /// the distance is always internally converted to the type of the pace.
    /// </summary>
    public class Calculations
    {
        public const double kilometersToMiles = 0.621371;
        public const double milesToKilometers = 1.60934;
        Data data = null;

        // create event handlers
        public delegate void PaceChangeHandler(Object sender, PaceEventArgs e);
        public static event PaceChangeHandler OnPaceChange;
        public delegate void TimeChangeHandler(Object sender, TimeEventArgs e);
        public static event TimeChangeHandler OnTimeChange;
        public delegate void DistanceChangeHandler(Object sender, DistanceEventArgs e);
        public static event DistanceChangeHandler OnDistanceChange;
        public delegate void SplitChangeHandler(Object sender, SplitEventArgs e);
        public static event SplitChangeHandler onSplitChange;

        public Calculations(Data data)
        {
            this.data = data;
        }

        public Boolean DoCalculation()
        {
            if (data.ValidBaseData() == false)
            {
                return false;
            }

            if (data.CalcPace)
            {
                return CalcPace();
            }
            else if (data.CalcDist)
            {
                return CalcDistance();
            }
            else if (data.CalcTime)
            {
                return CalcTime();
            }

            return false;
        }

        public Boolean CalcPace()
        {
            double totalSeconds = 0;
            double averageSeconds = 0;
            double remainingSeconds = 0;
            double distance = ConvertDistanceToPaceType(data.Distance);


            if (data.ValidBaseData() == false || distance == 0)
            {
                data.PaceHours = 0;
                data.PaceMinutes = 0;
                data.PaceSeconds = 0;

                return false;
            }

            totalSeconds = ((double)data.Hours * 60 * 60 + data.Minutes * 60) + data.Seconds;

            averageSeconds = totalSeconds / distance;

            data.PaceHours = (int)(averageSeconds / (60 * 60));
            remainingSeconds = averageSeconds - (data.PaceHours * 60 * 60);

            data.PaceMinutes = (int)(remainingSeconds / 60);
            remainingSeconds = remainingSeconds - (data.PaceMinutes * 60);

            data.PaceSeconds = remainingSeconds;

            // making this event a bit more thread safe by assigning the
            // event to a variable.  As we are never removing the handler really
            // isn't necessary but not a bad reminder.
            PaceChangeHandler omc = OnPaceChange;

            // only update if there is at least one listener active
            if (omc != null)
            {
                omc(this, new PaceEventArgs(data.PaceHours, data.PaceMinutes, data.PaceSeconds));
            }

            return true;
        }

        public Boolean CalcDistance()
        {
            double totalSeconds = 0;
            double totalPaceSeconds = 0;

            if (data.ValidBaseData() == false)
            {
                data.Distance = 0;

                return false;
            }

            totalSeconds = ((double)data.Hours * 60 * 60 + data.Minutes * 60) + data.Seconds;
            totalPaceSeconds = ((double)data.PaceHours * 60 * 60 + data.PaceMinutes * 60) + data.PaceSeconds;

            if (totalPaceSeconds != 0)
            {
                data.Distance = totalSeconds / totalPaceSeconds;
            }
            else
            {
                return false;
            }

            data.Distance = ConvertDistanceToDistanceType(data.Distance);
            
            // making this event a bit more thread safe by assigning the
            // event to a variable.  As we are never removing the handler really
            // isn't necessary but not a bad reminder.
            DistanceChangeHandler omc = OnDistanceChange;

            // only update if there is at least one listener active
            if (omc != null)
            {
                omc(this, new DistanceEventArgs(data.Distance));
            }

            return true;
        }

        public Boolean CalcTime()
        {
            double totalSeconds = 0;
            double averageSeconds = 0;
            double remainingSeconds = 0;
            double distance = 0;

            if (data.ValidBaseData() == false)
            {
                data.Hours = 0;
                data.Minutes = 0;
                data.Seconds = 0;

                return false;
            }

            distance = ConvertDistanceToPaceType(data.Distance);

            averageSeconds = ((double)data.PaceHours * 60 * 60 + data.PaceMinutes * 60) + data.PaceSeconds;
            totalSeconds = averageSeconds * distance;

            data.Hours = (int)(totalSeconds / (60 * 60));
            remainingSeconds = totalSeconds - (data.Hours * 60 * 60);

            data.Minutes = (int)(remainingSeconds / 60);
            remainingSeconds = remainingSeconds - (data.Minutes * 60);

            data.Seconds = remainingSeconds;

            // making this event a bit more thread safe by assigning the
            // event to a variable.  As we are never removing the handler really
            // isn't necessary but not a bad reminder.
            TimeChangeHandler omc = OnTimeChange;

            // only update if there is at least one listener active
            if (omc != null)
            {
                omc(this, new TimeEventArgs(data.Hours, data.Minutes, data.Seconds));
            }

            return true;
        }

        public Boolean CalcSplits()
        {
            if (data.ValidExtendedData() == false)
            {
                return false;
            }

            double distance = ConvertDistanceToSplitDistanceType(data.Distance);
            int numberOfSplits = (int) Math.Ceiling(distance / data.SplitDistance);
            double totalSeconds = ((double)data.Hours * 60 * 60 + data.Minutes * 60) + data.Seconds;
            double splitDistance = data.SplitDistance;
            double perSplitSeconds = totalSeconds / (distance / splitDistance);
            int splitHours = 0;
            int splitMinutes = 0;
            double splitSeconds = 0;
            double accumlativeSplitDistance = 0;
            double accumlativeSplitTime = 0;

            // reset the splits
            data.SplitList.Clear();

            for (int splitCount = 0; splitCount < numberOfSplits; splitCount += 1)
            {
                accumlativeSplitDistance = splitDistance * (splitCount + 1);

                if (accumlativeSplitDistance > distance)
                {
                    accumlativeSplitDistance = distance;
                }

                accumlativeSplitTime = perSplitSeconds * (accumlativeSplitDistance / splitDistance);

                splitHours = (int)(accumlativeSplitTime / (60 * 60));
                accumlativeSplitTime = accumlativeSplitTime - (splitHours * 60 * 60);

                splitMinutes = (int)(accumlativeSplitTime / 60);
                accumlativeSplitTime = accumlativeSplitTime - (splitMinutes * 60);

                splitSeconds = accumlativeSplitTime;

                data.SplitList.Add(new Split(splitHours, splitMinutes, splitSeconds, accumlativeSplitDistance));
            }

            // making this event a bit more thread safe by assigning the
            // event to a variable.  As we are never removing the handler really
            // isn't necessary but not a bad reminder.
            SplitChangeHandler omc = onSplitChange;

            // only update if there is at least one listener active
            if (omc != null)
            {
                omc(this, new SplitEventArgs(data.SplitList));
            }

            return true;
        }

        public Boolean ClearSplits()
        {
            data.SplitList.Clear();

            // making this event a bit more thread safe by assigning the
            // event to a variable.  As we are never removing the handler really
            // isn't necessary but not a bad reminder.
            SplitChangeHandler omc = onSplitChange;

            // only update if there is at least one listener active
            if (omc != null)
            {
                omc(this, new SplitEventArgs(data.SplitList));
            }

            return true;
        }

        /// <summary>
        /// When calculating the distance, distance is in the units of the pace BUT if the distance type
        /// does not match the pace type then a conversion to the distance type is needed.
        /// </summary>
        /// <param name="distanceInput">The distance in pace type units.</param>
        /// <returns>Distance in the units of the distance type.</returns>
        public double ConvertDistanceToDistanceType(double distanceInput)
        {
            double distance = distanceInput;

            if (data.DistanceType == Data.DistanceTypes.kilometers && data.PaceType == Data.PaceTypes.minutesPerMile)
            {
                distance *= milesToKilometers;
            }

            if (data.DistanceType == Data.DistanceTypes.miles && data.PaceType == Data.PaceTypes.minutesPerKilometer)
            {
                distance *= kilometersToMiles;
            }

            return distance;
        }

        /// <summary>
        /// When calculating time/pace the distance needs to match the units of pace.  This method does that.
        /// </summary>
        /// <param name="distanceInput">The distance in its units.</param>
        /// <returns>Distance in the units of the pace type.</returns>
        public double ConvertDistanceToPaceType(double distanceInput)
        {
            double distance = distanceInput;

            if (data.DistanceType == Data.DistanceTypes.kilometers && data.PaceType == Data.PaceTypes.minutesPerMile)
            {
                distance *= kilometersToMiles;
            }

            if (data.DistanceType == Data.DistanceTypes.miles && data.PaceType == Data.PaceTypes.minutesPerKilometer)
            {
                distance *= milesToKilometers;
            }

            return distance;
        }

        /// <summary>
        /// When calculating splits the distance needs to match the units of the split.  This method does that.
        /// </summary>
        /// <param name="distanceInput">The distance in its units.</param>
        /// <returns>Distance in the units of the split distance type.</returns>
        public double ConvertDistanceToSplitDistanceType(double distanceInput)
        {
            double distance = distanceInput;

            if (data.DistanceType == Data.DistanceTypes.kilometers && data.SplitDistanceType == Data.DistanceTypes.miles)
            {
                distance *= kilometersToMiles;
            }

            if (data.DistanceType == Data.DistanceTypes.miles && data.SplitDistanceType == Data.DistanceTypes.kilometers)
            {
                distance *= milesToKilometers;
            }

            return distance;
        }
    }
}
