using System;
using Model;
using System.Windows;
using System.Collections.ObjectModel;

namespace ViewModels
{
    public class WpfVM : ObservableObject
    {
        private string debug = string.Empty;
        private string distance = string.Empty;
        private string time = string.Empty;
        private string pace = string.Empty;
        private Boolean calcDistance;
        private Boolean calcTime;
        private Boolean calcPace;
        private string distanceType = string.Empty;
        private string paceType = string.Empty;
        private Data data;
        private string splitDistance = string.Empty;
        private string splitDistanceType = string.Empty;
        private ObservableCollection<Split> splitList;
        private Visibility showSplitGrid;
        private readonly DelegateCommand showSplitsCommand;
        private readonly DelegateCommand calculateTypeCommand;

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public WpfVM()
        {
            // create a data object to work with.
            data = new Data();

            // let's set some default values
            showSplitsCommand = new DelegateCommand(ShowSplits, ShowSplitsEnable);
            calculateTypeCommand = new DelegateCommand(CalculateType, ShowCalculateEnable);
            CalcPace = true;
        }

        // Begin Properties

        /// <summary>
        /// Distance type property.  
        /// One of the Model.Data.DistanceTypes returned as a string.
        /// A string that is converted to a Model.Data.DistanceTypes.
        /// </summary>
        public string DistanceType
        {
            get { return distanceType; }
            set
            {
                Data.DistanceTypes dType = Data.DistanceTypes.kilometers;
                distanceType = value;

                if (ParseStrings.ParseDistanceType(distanceType, ref dType))
                {
                    data.DistanceType = dType;
                }

                distanceType = data.DistanceType.ToString();

                DebugText = distanceType;
                RaisePropertyChangedEvent("DistanceType");
            }
        }

        /// <summary>
        /// Split distance type property.
        /// One of the Model.Data.DistanceTypes returned as a string.
        /// A string that is converted to a Model.Data.DistanceTypes.
        /// </summary>
        public string SplitDistanceType
        {
            get { return splitDistanceType; }
            set
            {
                Data.DistanceTypes dType = Data.DistanceTypes.kilometers;
                splitDistanceType = value;

                if (ParseStrings.ParseDistanceType(splitDistanceType, ref dType))
                {
                    data.SplitDistanceType = dType;
                }

                splitDistanceType = data.SplitDistanceType.ToString();

                DebugText = splitDistanceType;
                RaisePropertyChangedEvent("SplitDistanceType");
            }
        }

        /// <summary>
        /// Pace type property.
        /// One of the Model.Data.PaceTypes returned as a string.
        /// A string that is converted to a Model.Data.PaceTypes.
        /// </summary>
        public string PaceType
        {
            get { return paceType; }
            set
            {
                Data.PaceTypes pType = Data.PaceTypes.minutesPerKilometer;
                paceType = value;

                if (ParseStrings.ParsePaceType(paceType, ref pType))
                {
                    data.PaceType = pType;
                }

                paceType = data.PaceType.ToString();

                DebugText = paceType;
                RaisePropertyChangedEvent("PaceType");
            }
        }

        /// <summary>
        /// Calculate distance boolean property.
        /// True if we are to calculate the distance.
        /// False if we are not to calculate the distance.
        /// </summary>
        public Boolean CalcDistance
        {
            get { return calcDistance; }

            set
            {
                if (value == calcDistance) return;

                calcDistance = value;
                data.CalcDist = calcDistance;
                DebugText += " CalcDistance: " + calcDistance.ToString();
                RaisePropertyChangedEvent("CalcDistance");
                calculateTypeCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Calculate time boolean property.
        /// True if we are to calculate the time.
        /// False if we are not to calculate the time.
        /// </summary>
        public Boolean CalcTime
        {
            get
            {
                return calcTime;
            }

            set
            {
                if (value == calcTime)
                {
                    return;
                }

                calcTime = value;
                data.CalcTime = calcTime;
                DebugText += " CalcTime: " + calcTime.ToString();
                RaisePropertyChangedEvent("CalcTime");
                calculateTypeCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Calculate pace boolean property.
        /// True if we are to calculate the pace.
        /// False if we are not to calculate the pace.
        /// </summary>
        public Boolean CalcPace
        {
            get
            {
                return calcPace;
            }

            set
            {
                if (value == calcPace)
                {
                    return;
                }

                calcPace = value;
                data.CalcPace = calcPace;
                DebugText += " CalcPace: " + calcPace.ToString();
                RaisePropertyChangedEvent("CalcPace");
                calculateTypeCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Property for debug information just to be used during development.
        /// </summary>
        public string DebugText
        {
            get
            {
                return debug;
            }
            set
            {
                if (value == debug)
                {
                    return;
                }

                debug = value;
                RaisePropertyChangedEvent("DebugText");
            }
        }

        /// <summary>
        /// Distance property.
        /// Returns a string representation of the distance.
        /// Takes a string and converts it to a double value for distance.
        /// </summary>
        public string Distance
        {
            get
            {
                return distance;
            }
            set
            {
                string test = value;

                if (value == distance)
                {
                    return;
                }

                double distanceValue = 0;

                if (value.Equals(string.Empty))
                {
                    data.Distance = 0;
                    distance = value;
                    RaisePropertyChangedEvent("Distance");
                }
                else if (ParseStrings.ParseDistanceString(test, ref distanceValue))
                {
                    data.Distance = distanceValue;
                    distance = test;
                    RaisePropertyChangedEvent("Distance");
                }

                calculateTypeCommand.RaiseCanExecuteChanged();
                DebugText = distance;
            }
        }

        public string SplitDistance
        {
            get
            {
                return splitDistance;
            }
            set
            {
                if (value == splitDistance)
                {
                    return;
                }

                double splitDistanceValue = 0;

                if (value.Equals(string.Empty))
                {
                    data.SplitDistance = 0;
                    splitDistance = value;
                    RaisePropertyChangedEvent("SplitDistance");
                    
                    // set up listener for split change
                    Calculations.onSplitChange += new Calculations.SplitChangeHandler(OnSplitChange);

                    Calculations calc = new Calculations(data);
                    calc.ClearSplits();
                }
                else if (ParseStrings.ParseSplitDistanceString(value, ref splitDistanceValue))
                {
                    data.SplitDistance = splitDistanceValue;
                    splitDistance = value;
                    RaisePropertyChangedEvent("SplitDistance");
                }

                showSplitsCommand.RaiseCanExecuteChanged();
                DebugText = splitDistance;
            }
        }

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                if (value == time)
                {
                    return;
                }

                int hours = 0;
                int minutes = 0;
                double seconds = 0;

                if (value.Equals(string.Empty))
                {
                    data.Hours = 0;
                    data.Minutes = 0;
                    data.Seconds = 0;

                    time = value;
                    RaisePropertyChangedEvent("Time");
                }
                else if (ParseStrings.ParseTimeString(value, ref hours, ref minutes, ref seconds))
                {
                    data.Hours = hours;
                    data.Minutes = minutes;
                    data.Seconds = seconds;

                    time = value;
                    RaisePropertyChangedEvent("Time");
                }

                calculateTypeCommand.RaiseCanExecuteChanged();
                DebugText = time;
            }
        }

        public string Pace
        {
            get
            {
                return pace;
            }
            set
            {
                if (value == pace)
                {
                    return;
                }

                int paceHours = 0;
                int paceMinutes = 0;
                double paceSeconds = 0;

                if (value.Equals(string.Empty))
                {
                    data.PaceHours = 0;
                    data.PaceMinutes = 0;
                    data.PaceSeconds = 0;

                    pace = value;
                    RaisePropertyChangedEvent("Pace");
                }
                else if (ParseStrings.ParseTimeString(value, ref paceHours, ref paceMinutes, ref paceSeconds))
                {
                    data.PaceHours = paceHours;
                    data.PaceMinutes = paceMinutes;
                    data.PaceSeconds = paceSeconds;

                    pace = value;
                    RaisePropertyChangedEvent("Pace");
                }

                calculateTypeCommand.RaiseCanExecuteChanged();
                DebugText = pace;
            }
        }

        /// <summary>
        /// Split grid visibility property
        /// </summary>
        public Visibility ShowSplitGrid
        {
            get { return showSplitGrid; }
            set { showSplitGrid = value; RaisePropertyChangedEvent("ShowSplitGrid"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Split> SplitList
        {
            get { return splitList; }
            set
            {
                splitList = value;
                RaisePropertyChangedEvent("SplitList");
            }
        }

        // end of properties

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand CalculateTypeCommand
        {
            get { return calculateTypeCommand; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CalculateType()
        {
            DebugText = "CalculateType command was hit.";
            // set up listeners
            Calculations.OnPaceChange += new Calculations.PaceChangeHandler(OnPaceChange);
            Calculations.OnTimeChange += new Calculations.TimeChangeHandler(OnTimeChange);
            Calculations.OnDistanceChange += new Calculations.DistanceChangeHandler(OnDistanceChange);

            // do calculations
            Calculations calcs = new Calculations(data);
            calcs.DoCalculation();

            // check if we have an active splits window ... if so also redo split calculations
            if (splitList != null && splitList.Count > 0)
            {
                calcs.CalcSplits();
            }
        }

        public Boolean ShowCalculateEnable()
        {
            Boolean isEnabled = false;

            if ((calcPace && distance != null && distance.Length > 0 && time != null && time.Length > 0) ||
                (calcTime && distance != null && distance.Length > 0 && pace != null && pace.Length > 0) ||
                (calcDistance && pace != null && pace.Length > 0 && time != null && time.Length > 0))
            {
                isEnabled = true;
            }

            return isEnabled;
        }

        /// <summary>
        /// Picks up pace changes from the model and sends them to the view.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pace"></param>
        public void OnPaceChange(object obj, PaceEventArgs pace)
        {
            Pace = CreateStrings.ConvertTimeToString(pace.Hours, pace.Minutes, pace.Seconds);
        }

        /// <summary>
        /// Picks up pace changes from the model and sends them to the view.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="time"></param>
        public void OnTimeChange(object obj, TimeEventArgs time)
        {
            Time = CreateStrings.ConvertTimeToString(time.Hours, time.Minutes, time.Seconds);
        }

        /// <summary>
        /// Picks up pace changes from the model and sends them to the view.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="distance"></param>
        public void OnDistanceChange(object obj, DistanceEventArgs distance)
        {
            Distance = CreateStrings.DistanceString(distance.Distance);
        }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand ShowSplitsCommand
        {
            get { return showSplitsCommand; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowSplits()
        {
            DebugText = "Show Splits command was hit.";

            // set up listeners
            Calculations.onSplitChange += new Calculations.SplitChangeHandler(OnSplitChange);

            Calculations calcs = new Calculations(data);
            calcs.CalcSplits();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean ShowSplitsEnable()
        {
            Boolean isEnabled = false;

            if (splitDistance != null && splitDistance.Length > 0)
            {
                isEnabled = true;
                ShowSplitGrid = Visibility.Visible;
            }
            else
            {
                ShowSplitGrid = Visibility.Hidden;
            }

            return isEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="splits"></param>
        public void OnSplitChange(object obj, SplitEventArgs splits)
        {
            SplitList = new ObservableCollection<Split>(splits.SplitList);
        }
    }
}
