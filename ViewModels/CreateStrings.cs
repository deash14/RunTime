using System;
using Model;

namespace ViewModels
{
    public static class CreateStrings
    {
        public static string ConvertTimeToString(int hours, int minutes, double seconds)
        {
            string time = string.Empty;

            if (hours > 0)
            {
                time = hours + ":";
            }

            if (minutes > 0)
            {
                if (time != string.Empty)
                {
                    time += minutes.ToString("00:");
                }
                else
                {
                    time += minutes.ToString("#0:");
                }
            }
            else if (time != string.Empty)
            {
                time += "00:";
            }

            if (seconds > 0)
            {
                time += seconds.ToString("00.##");
            }
            else if (time != string.Empty)
            {
                time += "00";
            }

            return time;
        }

        public static string DistanceString(double distanceData)
        {
            string distance = distanceData.ToString("0.##");

            return distance;
        }
    }
}
