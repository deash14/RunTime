using System;

namespace Model
{
    public static class ParseStrings
    {
        public static Boolean ParseDistanceType(string distanceTypeStr, ref Data.DistanceTypes distanceType)
        {
            Boolean success = false;
            Data.DistanceTypes testType;

            if (Enum.TryParse(distanceTypeStr, true, out testType))
            {
                distanceType = testType;
                success = true;
            }

            return success;
        }

        public static Boolean ParsePaceType(string paceTypeStr, ref Data.PaceTypes paceType)
        {
            Boolean success = false;
            Data.PaceTypes testType;

            if (Enum.TryParse(paceTypeStr, true, out testType))
            {
                paceType = testType;
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Parse a time string of [[hh:]mm:]ss[.dddddd] into integer hours, minutes, and double seconds.
        /// </summary>
        /// <param name="timeString">The string to parse.</param>
        /// <param name="hours">hours to return.</param>
        /// <param name="minutes">minutes to return.</param>
        /// <param name="seconds">seconds to return.</param>
        /// <returns>
        /// true if no errors were found, false otherwise.
        /// In the case of false the hours, minutes, and seconds will be set to zero.
        /// </returns>
        public static bool ParseTimeString(string timeString, ref int hours, ref int minutes, ref double seconds)
        {
            bool retValue = true;
            char[] separators = { ':' };
            string[] timeParts = timeString.Split(separators);
            Boolean validHours = true;
            Boolean validMinutes = true;
            Boolean validSeconds = true;

            // initialize outputs
            hours = 0;
            minutes = 0;
            seconds = 0;

            Array.Reverse(timeParts);

            switch (timeParts.Length)
            {
                case 1:
                    validSeconds = double.TryParse(timeParts[0], out seconds);
                    break;

                case 2:
                    if (timeParts[0] == string.Empty)
                    {
                        seconds = 0;
                    }
                    else
                    {
                        validSeconds = double.TryParse(timeParts[0], out seconds);
                    }
                    validMinutes = int.TryParse(timeParts[1], out minutes);
                    break;
                case 3:
                    if (timeParts[0] == string.Empty)
                    {
                        seconds = 0;
                    }
                    else
                    {
                        validSeconds = double.TryParse(timeParts[0], out seconds);
                    }
                    validMinutes = int.TryParse(timeParts[1], out minutes);
                    validHours = int.TryParse(timeParts[2], out hours);
                    break;
                default:
                    validSeconds = false;
                    validMinutes = false;
                    validHours = false;
                    break;
            }

            // verify that we have good values
            if (hours < 0 || minutes < 0 || seconds < 0 ||
                !validHours || !validMinutes || !validSeconds)
            {
                hours = 0;
                minutes = 0;
                seconds = 0;
                retValue = false;
            }

            return retValue;
        }

        public static bool ParseDistanceString(string distanceString, ref double distance)
        {
            bool retValue = false;

            if (Double.TryParse(distanceString, out distance))
            {
                retValue = true;
            }

            if (distance < 0)
            {
                distance = 0;
                retValue = false;
            }

            return retValue;
        }

        public static bool ParseSplitDistanceString(string splitDistanceString, ref double splitDistance)
        {
            bool retValue = false;

            if (Double.TryParse(splitDistanceString, out splitDistance))
            {
                retValue = true;
            }

            if (splitDistance < 0)
            {
                splitDistance = 0;
                retValue = false;
            }

            return retValue;
        }
    }
}
