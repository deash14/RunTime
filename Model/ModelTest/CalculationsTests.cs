using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model.Tests
{
    [TestClass()]
    public class CalculationsTests
    {
        private TestContext testContextInstance;
        public int PaceHours { get; set; }
        public int PaceMinutes { get; set; }
        public double PaceSeconds { get; set; }
        public double Distance { get; set; }
        public int TimeHours { get; set; }
        public int TimeMinutes { get; set; }
        public double TimeSeconds { get; set; }
        public List<Split> Splits { get; set; }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        // helper methods here

        /// <summary>
        /// Turns a string representing a paceType enumeration into a paceType
        /// </summary>
        /// <param name="calcType"></param>
        public void ConvertCalcTypeString(Data data, string calcType, out Boolean validData)
        {
            validData = true;

            switch (calcType)
            {
                case "pace":
                    data.CalcPace = true;
                    break;
                case "time":
                    data.CalcTime = true;
                    break;
                case "distance":
                    data.CalcDist = true;
                    break;
                case "invalid":
                    validData = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Turns a string representing a paceType enumeration into a paceType
        /// </summary>
        /// <param name="paceType"></param>
        public Data.PaceTypes ConvertPaceTypeString(string paceType)
        {
            Data.PaceTypes pt;

            switch (paceType)
            {
                case "perMile":
                    pt = Data.PaceTypes.minutesPerMile;
                    break;
                case "perKm":
                    pt = Data.PaceTypes.minutesPerKilometer;
                    break;
                default:
                    pt = Data.PaceTypes.minutesPerKilometer;
                    break;
            }

            return pt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distanceType"></param>
        public Data.DistanceTypes ConvertDistanceTypeString(string distanceType)
        {
            Data.DistanceTypes dt;

            switch (distanceType)
            {
                case "miles":
                    dt = Data.DistanceTypes.miles;
                    break;
                case "km":
                    dt = Data.DistanceTypes.kilometers;
                    break;
                default:
                    dt = Data.DistanceTypes.kilometers;
                    break;
            }

            return dt;
        }

        public List<Split> ConvertSplitsFromCsvToList(string datafile)
        {
            double distance = -1;
            int hours = -1;
            int minutes = -1;
            double seconds = -1;

            List<Split> splits = new List<Split>();

            String[] lines = File.ReadAllLines(datafile);

            foreach (String line in lines)
            {
                var fields = line.Split(',');

                if (fields.Length >= 4)
                {
                    distance = Convert.ToDouble(fields[0]);
                    hours = Convert.ToInt32(fields[1]);
                    minutes = Convert.ToInt32(fields[2]);
                    seconds = Convert.ToDouble(fields[3]);
                }

                splits.Add(new Split(hours, minutes, seconds, distance));
            }

            return splits;
        }

        // End of Helper methods, start of unit tests

        [DeploymentItem(@"../../UnitTestData/DoCalculationTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\DoCalculationTest.csv",
                    "DoCalculationTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void DoCalculationTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            string calcType = Convert.ToString(TestContext.DataRow["CalcType"]);
            data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            data.PaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            data.PaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            data.PaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["ExpectedDistance"]);
            int expectedHours = Convert.ToInt32(TestContext.DataRow["expectedHours"]);
            int expectedMinutes = Convert.ToInt32(TestContext.DataRow["expectedMinutes"]);
            double expectedSeconds = Convert.ToDouble(TestContext.DataRow["ExpectedSeconds"]);
            Boolean validData;

            // convert test data
            ConvertCalcTypeString(data, calcType, out validData);
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.PaceType = ConvertPaceTypeString(paceType);

            // run test
            calcs.DoCalculation();

            if (!validData)
            {
                expectedDistance = 0;
                expectedHours = 0;
                expectedMinutes = 0;
                expectedSeconds = 0;
            }

            // validate results
            if (data.CalcDist)
            {
                Assert.AreEqual(expectedDistance, Math.Round(data.Distance, 2), "Distance differed.");
            }

            if (data.CalcPace)
            {
                Assert.AreEqual(expectedHours, data.PaceHours, "Pace Hours differed.");
                Assert.AreEqual(expectedMinutes, data.PaceMinutes, "Pace Minutes differed.");
                Assert.AreEqual(expectedSeconds, Math.Round(data.PaceSeconds, 6), "Pace Seconds differed.");
            }

            if (data.CalcTime)
            {
                Assert.AreEqual(expectedHours, data.Hours, "Time Hours differed.");
                Assert.AreEqual(expectedMinutes, data.Minutes, "Time Minutes differed.");
                Assert.AreEqual(expectedSeconds, Math.Round(data.Seconds, 4), "Time Seconds differed.");
            }
        }

        [TestMethod()]
        public void CalcPaceHandlerTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.Hours = 1;
            data.Minutes = 29;
            data.Seconds = 59;
            data.Distance = 13.1;
            data.DistanceType = Data.DistanceTypes.miles;
            data.PaceType = Data.PaceTypes.minutesPerMile;
            int expectedPaceHours = 0;
            int expectedPaceMinutes = 6;
            double expectedPaceSeconds = 52.137405;

            Calculations.OnPaceChange += new Calculations.PaceChangeHandler(OnPaceChange);

            // run test
            calcs.CalcPace();

            // validate results
            Assert.AreEqual(expectedPaceHours, PaceHours, "Pace Hours differed.");
            Assert.AreEqual(expectedPaceMinutes, PaceMinutes, "Pace Minutes differed.");
            Assert.AreEqual(expectedPaceSeconds, Math.Round(PaceSeconds, 6), "Pace Seconds differed.");

            // remove the listener
            Calculations.OnPaceChange -= new Calculations.PaceChangeHandler(OnPaceChange);
        }

        /// <summary>
        /// Picks up pace changes from the model
        /// </summary>
        /// <param name="pace"></param>
        public void OnPaceChange(object obj, PaceEventArgs pace)
        {
            PaceHours = pace.Hours;
            PaceMinutes = pace.Minutes;
            PaceSeconds = pace.Seconds;
        }

        [TestMethod()]
        public void CalcDistanceHandlerTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.Hours = 1;
            data.Minutes = 29;
            data.Seconds = 59;
            data.DistanceType = Data.DistanceTypes.miles;
            data.PaceType = Data.PaceTypes.minutesPerMile;
            data.PaceHours = 0;
            data.PaceMinutes = 6;
            data.PaceSeconds = 52.137405;
            double expectedDistance = 13.1;

            Calculations.OnDistanceChange += new Calculations.DistanceChangeHandler(OnDistanceChange);

            // run test
            calcs.CalcDistance();

            // validate results
            Assert.AreEqual(expectedDistance, Math.Round(Distance, 2), "Distance differed.");

            // remove the listener
            Calculations.OnDistanceChange -= new Calculations.DistanceChangeHandler(OnDistanceChange);
        }

        /// <summary>
        /// Picks up distance changes for the model
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="distance"></param>
        public void OnDistanceChange(object obj, DistanceEventArgs distance)
        {
            Distance = distance.Distance;
        }

        [TestMethod()]
        public void CalcTimeHandlerTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.PaceHours = 0;
            data.PaceMinutes = 6;
            data.PaceSeconds = 52.137405;
            data.Distance = 13.1;
            data.DistanceType = Data.DistanceTypes.miles;
            data.PaceType = Data.PaceTypes.minutesPerMile;
            int expectedHours = 1;
            int expectedMinutes = 29;
            double expectedSeconds = 59;

            Calculations.OnTimeChange += new Calculations.TimeChangeHandler(OnTimeChange);

            // run test
            calcs.CalcTime();

            // validate results
            Assert.AreEqual(expectedHours, TimeHours, "Time Hours differed.");
            Assert.AreEqual(expectedMinutes, TimeMinutes, "Time Minutes differed.");
            Assert.AreEqual(expectedSeconds, Math.Round(TimeSeconds, 4), "Time Seconds differed.");

            // remove the listener
            Calculations.OnTimeChange -= new Calculations.TimeChangeHandler(OnTimeChange);
        }

        /// <summary>
        /// Picks up time changes from the model
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="time"></param>
        public void OnTimeChange(object obj, TimeEventArgs time)
        {
            TimeHours = time.Hours;
            TimeMinutes = time.Minutes;
            TimeSeconds = time.Seconds;
        }

        [DeploymentItem(@"../../UnitTestData/calcSplitTest4.csv"), 
         TestMethod()]
        public void CalcSplitsHandlerTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.Hours = 1;
            data.Minutes = 29;
            data.Seconds = 59;
            data.Distance = 13.1;
            data.DistanceType = Data.DistanceTypes.kilometers;
            data.SplitDistanceType = Data.DistanceTypes.kilometers;
            data.SplitDistance = 5;
            string expectedDataFile = "calcSplitTest4.csv";
            Boolean expected = true;

            Calculations.onSplitChange += new Calculations.SplitChangeHandler(OnSplitChange);

            // run test
            Boolean actual = calcs.CalcSplits();

            Assert.AreEqual(expected, actual, "Mismatched return value.");

            // get the expected data
            List<Split> expectedData = ConvertSplitsFromCsvToList(expectedDataFile);

            // we also need to check that the length in the data.SplitList is correct.
            Assert.AreEqual(expectedData.Count, Splits.Count, "Mismatch in Split counts.");

            // and that the values are correct
            for (int index = 0; index < data.SplitList.Count; index += 1)
            {
                Assert.AreEqual(expectedData[index].Distance, Splits[index].Distance, "For index " + index + " distances mismatched.");
                Assert.AreEqual(expectedData[index].Hours, Splits[index].Hours, "For index " + index + " hours mismatched.");
                Assert.AreEqual(expectedData[index].Minutes, Splits[index].Minutes, "For index " + index + " minutes mismatched.");
                Assert.AreEqual(expectedData[index].Seconds.ToString("F2"), Splits[index].Seconds.ToString("F2"), "For index " + index + " seconds mismatched.");
            }

            // remove the listener
            Calculations.onSplitChange -= new Calculations.SplitChangeHandler(OnSplitChange);
        }

        public void OnSplitChange(object obj, SplitEventArgs splits)
        {
            Splits = splits.SplitList;
        }

        [DeploymentItem(@"../../UnitTestData/CalcPaceTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\CalcPaceTest.csv",
                    "CalcPaceTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void CalcPaceTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            int expectedPaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            int expectedPaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            double expectedPaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.PaceType = ConvertPaceTypeString(paceType);

            // run test
            calcs.CalcPace();

            // validate results
            Assert.AreEqual(expectedPaceHours, data.PaceHours, "Pace Hours differed.");
            Assert.AreEqual(expectedPaceMinutes, data.PaceMinutes, "Pace Minutes differed.");
            Assert.AreEqual(expectedPaceSeconds, Math.Round(data.PaceSeconds, 6), "Pace Seconds differed.");
        }

        [DeploymentItem(@"../../UnitTestData/CalcDistanceTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\CalcDistanceTest.csv",
                    "CalcDistanceTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void CalcDistanceTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            data.PaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            data.PaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            data.PaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["Distance"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.PaceType = ConvertPaceTypeString(paceType);

            // run test
            calcs.CalcDistance();

            // validate results
            Assert.AreEqual(expectedDistance, Math.Round(data.Distance, 2), "Distance differed.");
        }

        [DeploymentItem(@"../../UnitTestData/CalcTimeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\CalcTimeTest.csv",
                    "CalcTimeTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void CalcTimeTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.PaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            data.PaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            data.PaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);
            data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            int expectedHours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            int expectedMinutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            double expectedSeconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.PaceType = ConvertPaceTypeString(paceType);

            // run test
            calcs.CalcTime();

            // validate results
            Assert.AreEqual(expectedHours, data.Hours, "Time Hours differed.");
            Assert.AreEqual(expectedMinutes, data.Minutes, "Time Minutes differed.");
            Assert.AreEqual(expectedSeconds, Math.Round(data.Seconds, 4), "Time Seconds differed.");
        }

        [DeploymentItem(@"../../UnitTestData/calcSplitTest0.csv"),
         DeploymentItem(@"../../UnitTestData/calcSplitTest1.csv"),
         DeploymentItem(@"../../UnitTestData/calcSplitTest2.csv"),
         DeploymentItem(@"../../UnitTestData/calcSplitTest3.csv"),
         DeploymentItem(@"../../UnitTestData/calcSplitTest4.csv"),
         DeploymentItem(@"../../UnitTestData/calcSplitsTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\CalcSplitsTest.csv",
                    "CalcSplitsTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void CalcSplitsTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string splitDistanceType = Convert.ToString(TestContext.DataRow["SplitDistanceType"]);
            data.SplitDistance = Convert.ToDouble(TestContext.DataRow["SplitDistance"]);
            Boolean initializeSplitData = Convert.ToBoolean(TestContext.DataRow["initialSplitData"]);
            string expectedDataFile = Convert.ToString(TestContext.DataRow["ExpectedDataFile"]);
            Boolean expected = Convert.ToBoolean(TestContext.DataRow["Expected"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.SplitDistanceType = ConvertDistanceTypeString(splitDistanceType);

            // data.SplitList should not have any old data in it.  This test checks that by 
            // placing some data in it and verifying that it doesn't exist after the method call
            if (initializeSplitData)
            {
                data.SplitList.Add(new Split(0, 6, 52.2, 1));
                data.SplitList.Add(new Split(0, 13, 44.4, 2));
            }

            // run test
            Boolean actual = calcs.CalcSplits();

            Assert.AreEqual(expected, actual, "Mismatched return value.");

            // get the expected data
            List<Split> expectedData = ConvertSplitsFromCsvToList(expectedDataFile);

            // we also need to check that the length in the data.SplitList is correct.
            Assert.AreEqual(expectedData.Count, data.SplitList.Count, "Mismatch in Split counts.");

            // and that the values are correct
            for (int index = 0; index < data.SplitList.Count; index += 1)
            {
                Assert.AreEqual(expectedData[index].Distance, data.SplitList[index].Distance, "For index " + index + " distances mismatched.");
                Assert.AreEqual(expectedData[index].Hours, data.SplitList[index].Hours, "For index " + index + " hours mismatched.");
                Assert.AreEqual(expectedData[index].Minutes, data.SplitList[index].Minutes, "For index " + index + " minutes mismatched.");
                Assert.AreEqual(expectedData[index].Seconds.ToString("F2"), data.SplitList[index].Seconds.ToString("F2"), "For index " + index + " seconds mismatched.");
            }
        }

        [TestMethod]
        public void CalcClearSplitsTest()
        {
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            data.SplitList = new List<Split>();
            data.SplitList.Add(new Split(1, 29, 59, 13.1));

            Assert.IsTrue(data.SplitList.Count > 0);

            calcs.ClearSplits();

            Assert.IsFalse(data.SplitList.Count > 0);
        }

        [TestMethod]
        public void CalcClearSplitsHandlerTest()
        {
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            data.SplitList = new List<Split>();
            data.SplitList.Add(new Split(1, 29, 59, 13.1));

            Assert.IsTrue(data.SplitList.Count > 0);

            Calculations.onSplitChange += new Calculations.SplitChangeHandler(OnSplitChange);

            calcs.ClearSplits();

            Assert.IsFalse(data.SplitList.Count > 0);
        }

        [DeploymentItem(@"../../UnitTestData/SanitizeDistanceForSplitsTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\SanitizeDistanceForSplitsTest.csv",
                    "SanitizeDistanceForSplitsTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ConvertDistanceToSplitDistanceTypeTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string splitDistanceType = Convert.ToString(TestContext.DataRow["SplitDistanceType"]);
            double distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["Expected"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.SplitDistanceType = ConvertDistanceTypeString(splitDistanceType);

            // run test
            double actualDistance = Math.Round(calcs.ConvertDistanceToSplitDistanceType(distance), 3);

            Assert.AreEqual(expectedDistance, actualDistance);
        }

        [DeploymentItem(@"../../UnitTestData/SanitizeDistanceForTime.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\SanitizeDistanceForTime.csv",
                    "SanitizeDistanceForTime#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ConvertDistanceToPaceTypeTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            double distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["Expected"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.PaceType = ConvertPaceTypeString(paceType);

            // run test
            double actualDistance = Math.Round(calcs.ConvertDistanceToPaceType(distance), 3);

            Assert.AreEqual(expectedDistance, actualDistance);
        }

        [DeploymentItem(@"../../UnitTestData/SanitizeDistanceForDistance.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\SanitizeDistanceForDistance.csv",
                    "SanitizeDistanceForDistance#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ConvertDistanceToDistanceTypeTest()
        {
            // create a Data & Calculation object
            Data data = new Data();
            Calculations calcs = new Calculations(data);

            // get test data
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            double distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["Expected"]);

            // convert test data
            data.DistanceType = ConvertDistanceTypeString(distanceType);
            data.PaceType = ConvertPaceTypeString(paceType);

            // run test
            double actualDistance = Math.Round(calcs.ConvertDistanceToDistanceType(distance), 3);

            Assert.AreEqual(expectedDistance, actualDistance);
        }
    }
}