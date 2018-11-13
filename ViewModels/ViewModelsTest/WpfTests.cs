using ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Model;

namespace ViewModels.Tests
{
    [TestClass()]
    public class WpfTests
    {
        private TestContext testContextInstance;

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

        // helper methods

        public Visibility ConvertStringToVisibilityType(string visibitityType)
        {
            Visibility visibility = Visibility.Visible;

            switch (visibitityType)
            {
                case "collasped":
                    visibility = Visibility.Collapsed;
                    break;
                case "hidden":
                    visibility = Visibility.Hidden;
                    break;
                case "visible":
                    visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            return visibility;
        }

        public void ConvertCalcType(WpfVM wpf, string calcType)
        {
            switch (calcType)
            {
                case "time":
                    wpf.CalcTime = true;
                    wpf.CalcDistance = false;
                    wpf.CalcPace = false;
                    break;
                case "pace":
                    wpf.CalcTime = false;
                    wpf.CalcDistance = false;
                    wpf.CalcPace = true;
                    break;
                case "distance":
                    wpf.CalcTime = false;
                    wpf.CalcDistance = true;
                    wpf.CalcPace = false;
                    break;
                default:
                    break;
            }
        }

        // end helper methods

        [TestMethod()]
        public void WpfTest()
        {
            WpfVM wpf = new WpfVM();

            // these are the only things that are really defaults at the creation of a Wpf object.
            Assert.AreEqual(wpf.CalcPace, true);
            Assert.AreEqual(wpf.CalcTime, false);
            Assert.AreEqual(wpf.CalcDistance, false);
        }

        [DeploymentItem(@"../../UnitTestData/DistanceTypeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\DistanceTypeTest.csv",
                    "DistanceTypeTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void DistanceTypeSetTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string distanceType = Convert.ToString(TestContext.DataRow["distanceType"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);

            // run test
            wpf.DistanceType = distanceType;

            // validate test
            Assert.AreEqual(expected, wpf.DistanceType);
        }

        /// <summary>
        /// This test is covering the branch of the ObserableObject class where a listener exists.
        /// </summary>
        [TestMethod]
        public void ObservableObjectTest()
        {
            WpfVM wpf = new WpfVM();

            // set up a listener
            wpf.PropertyChanged += Wpf_PropertyChanged;

            wpf.DistanceType = "test";

            // validate test
            Assert.AreEqual("kilometers", wpf.DistanceType);
        }

        /// <summary>
        /// Helper for the unit test ObservableObjectTest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wpf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        [DeploymentItem(@"../../UnitTestData/DistanceTypeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                   "|DataDirectory|\\DistanceTypeTest.csv",
                   "DistanceTypeTest#csv",
                   DataAccessMethod.Sequential),
        TestMethod()]
        public void SplitDistanceTypeSetTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string distanceType = Convert.ToString(TestContext.DataRow["distanceType"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);

            // run test
            wpf.SplitDistanceType = distanceType;

            // validate test
            Assert.AreEqual(expected, wpf.SplitDistanceType);
        }

        [DeploymentItem(@"../../UnitTestData/PaceTypeSetTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\PaceTypeSetTest.csv",
                    "PaceTypeSetTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod]
        public void PaceTypeSetTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string paceType = Convert.ToString(TestContext.DataRow["paceType"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);

            // run test
            wpf.PaceType = paceType;

            // validate test
            Assert.AreEqual(expected, wpf.PaceType);
        }

        [DeploymentItem(@"../../UnitTestData/BooleanSetTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\BooleanSetTest.csv",
                    "BooleanSetTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void CalcDistanceTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            Boolean expected = Convert.ToBoolean(TestContext.DataRow["expected"]);

            // run test
            wpf.CalcDistance = expected;

            // validate test
            Assert.AreEqual(expected, wpf.CalcDistance);
        }

        [DeploymentItem(@"../../UnitTestData/BooleanSetTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\BooleanSetTest.csv",
                    "BooleanSetTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void CalcTimeTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            Boolean expected = Convert.ToBoolean(TestContext.DataRow["expected"]);

            // run test
            wpf.CalcTime = expected;

            // validate test
            Assert.AreEqual(expected, wpf.CalcTime);
        }

        [DeploymentItem(@"../../UnitTestData/BooleanSetTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\BooleanSetTest.csv",
                    "BooleanSetTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void CalcPaceTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            Boolean expected = Convert.ToBoolean(TestContext.DataRow["expected"]);

            // run test
            wpf.CalcPace = expected;

            // validate test
            Assert.AreEqual(expected, wpf.CalcPace);
        }

        [DeploymentItem(@"../../UnitTestData/DistanceTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\DistanceTest.csv",
                    "DistanceTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void DistanceTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string distance = Convert.ToString(TestContext.DataRow["distance"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);

            // run test
            wpf.Distance = distance;

            // validate test
            if (expected.Equals("null"))
            {
                Assert.IsTrue(string.IsNullOrEmpty(wpf.Distance));
            }
            else
            {
                Assert.AreEqual(expected, wpf.Distance);
            }
        }

        [TestMethod]
        public void DistanceTwiceTest()
        {
            WpfVM wpf = new WpfVM();
            String firstTest = "5";
            String empty = string.Empty;

            wpf.Distance = firstTest;

            Assert.AreEqual(firstTest, wpf.Distance);

            wpf.Distance = empty;

            Assert.AreEqual(empty, wpf.Distance);
        }

        [DeploymentItem(@"../../UnitTestData/DistanceTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\DistanceTest.csv",
                    "DistanceTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void SplitDistanceTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string distance = Convert.ToString(TestContext.DataRow["distance"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);

            // run test
            wpf.SplitDistance = distance;

            // validate test
            if (expected.Equals("null"))
            {
                Assert.IsTrue(string.IsNullOrEmpty(wpf.SplitDistance));
            }
            else
            {
                Assert.AreEqual(expected, wpf.SplitDistance);
            }
        }

        [TestMethod]
        public void SplitDistanceTwiceTest()
        {
            WpfVM wpf = new WpfVM();
            String firstTest = "5";
            String empty = string.Empty;

            wpf.SplitDistance = firstTest;
            Assert.AreEqual(firstTest, wpf.SplitDistance);

            wpf.SplitDistance = firstTest;
            Assert.AreEqual(firstTest, wpf.SplitDistance);

            wpf.SplitDistance = empty;
            Assert.AreEqual(empty, wpf.SplitDistance);
        }

        [TestMethod]
        public void PaceTwiceTest()
        {
            string firstTest = "6:52";
            string empty = string.Empty;
            WpfVM wpf = new WpfVM();

            wpf.Pace = firstTest;
            Assert.AreEqual(firstTest, wpf.Pace);

            wpf.Pace = firstTest;
            Assert.AreEqual(firstTest, wpf.Pace);

            wpf.Pace = empty;
            Assert.AreEqual(empty, wpf.Pace);
        }

        [DeploymentItem(@"../../UnitTestData/TimeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\TimeTest.csv",
                    "TimeTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void TimeTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string time = Convert.ToString(TestContext.DataRow["time"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);
            string comment = Convert.ToString(TestContext.DataRow["comment"]);

            // run test
            wpf.Time = time;

            // validate test
            Assert.AreEqual(expected, wpf.Time);
        }

        [TestMethod]
        public void TimeTwiceTest()
        {
            string firstTest = "6:52";
            string empty = string.Empty;
            WpfVM wpf = new WpfVM();

            wpf.Time = firstTest;
            Assert.AreEqual(firstTest, wpf.Time);

            wpf.Time = firstTest;
            Assert.AreEqual(firstTest, wpf.Time);

            wpf.Time = empty;
            Assert.AreEqual(empty, wpf.Time);
        }

        [DeploymentItem(@"../../UnitTestData/TimeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\TimeTest.csv",
                    "TimeTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void PaceTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string time = Convert.ToString(TestContext.DataRow["time"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);
            string comment = Convert.ToString(TestContext.DataRow["comment"]);

            if (expected.Equals("null"))
            {
                expected = null;
            }

            // run test
            wpf.Pace = time;

            // validate test
            Assert.AreEqual(expected, wpf.Pace);
        }

        [DeploymentItem(@"../../UnitTestData/ShowSplitGridTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ShowSplitGridTest.csv",
                    "ShowSplitGridTest#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void ShowSplitGridTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            string visibility = Convert.ToString(TestContext.DataRow["visibility"]);

            // get expected value
            Visibility expected = ConvertStringToVisibilityType(visibility);

            // run test
            wpf.ShowSplitGrid = expected;

            // validate test
            Assert.AreEqual(expected, wpf.ShowSplitGrid);
        }

        [TestMethod]
        public void SplitListTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            ObservableCollection<Split> splitList = new ObservableCollection<Split>();
            splitList.Add(new Split(0, 1, 15, 0.25));
            splitList.Add(new Split(0, 2, 39, 0.5));
            splitList.Add(new Split(0, 3, 45, 0.75));
            splitList.Add(new Split(0, 5, 0, 1));

            // run test
            wpf.SplitList = splitList;

            // validate test
            // first make sure counts are equal
            Assert.AreEqual(splitList.Count, wpf.SplitList.Count, "List count mismatch.");

            for (int index = 0; index < splitList.Count; index += 1)
            {
                Assert.AreEqual(splitList[0].Hours, wpf.SplitList[0].Hours, "Hours mismatch at index " + index);
                Assert.AreEqual(splitList[0].Minutes, wpf.SplitList[0].Minutes, "Minutes mismatch at index " + index);
                Assert.AreEqual(splitList[0].Seconds, wpf.SplitList[0].Seconds, "Seconds mismatch at index " + index);
                Assert.AreEqual(splitList[0].Distance, wpf.SplitList[0].Distance, "Distance mismatch at index " + index);
            }
        }

        [DeploymentItem(@"../../UnitTestData/CalculateTypeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\CalculateTypeTest.csv",
                    "CalculateTypeTest#csv",
         DataAccessMethod.Sequential),
         TestMethod()]
        public void CalculateTypeTest()
        {
            WpfVM wpf = new WpfVM();

            // get data
            wpf.Time = Convert.ToString(TestContext.DataRow["Time"]);
            wpf.Pace = Convert.ToString(TestContext.DataRow["Pace"]);
            wpf.Distance = Convert.ToString(TestContext.DataRow["Distance"]);
            wpf.DistanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            wpf.PaceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            string calcType = Convert.ToString(TestContext.DataRow["CalcType"]);
            string expected = Convert.ToString(TestContext.DataRow["Expected"]);

            ConvertCalcType(wpf, calcType);

            // run test
            wpf.CalculateTypeCommand.Execute(null);

            // validate test
            switch (calcType)
            {
                case "time":
                    Assert.AreEqual(expected, wpf.Time);
                    return;
                case "pace":
                    Assert.AreEqual(expected, wpf.Pace);
                    return;
                case "distance":
                    Assert.AreEqual(expected, wpf.Distance);
                    return;
                default:
                    break;
            }

            Assert.Fail();
        }

        [DeploymentItem(@"../../UnitTestData/CalculateTypeTestWithSplits.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\CalculateTypeTestWithSplits.csv",
                    "CalculateTypeTestWithSplits#csv",
         DataAccessMethod.Sequential),
         TestMethod]
        public void CalculateTypeTestWithSplits()
        {
            WpfVM wpf = new WpfVM();

            // get data
            wpf.Time = Convert.ToString(TestContext.DataRow["Time"]);
            wpf.Pace = Convert.ToString(TestContext.DataRow["Pace"]);
            wpf.Distance = Convert.ToString(TestContext.DataRow["Distance"]);
            wpf.DistanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);
            wpf.PaceType = Convert.ToString(TestContext.DataRow["PaceType"]);
            string calcType = Convert.ToString(TestContext.DataRow["CalcType"]);
            string expected = Convert.ToString(TestContext.DataRow["Expected"]);
            wpf.SplitDistance = "1";
            wpf.SplitDistanceType = wpf.DistanceType;
            ConvertCalcType(wpf, calcType);

            // run test without many splits first
            wpf.ShowSplitsCommand.Execute(null);

            Assert.IsTrue(wpf.SplitList.Count > 1);

            int hours = 0;
            int minutes = 0;
            double seconds = 0;

            ParseStrings.ParseTimeString(wpf.Time, ref hours, ref minutes, ref seconds);

            wpf.SplitDistance = Convert.ToString(TestContext.DataRow["SplitDistance"]);

            // run test second time to verify splits get recalculated
            wpf.CalculateTypeCommand.Execute(null);

            Assert.AreEqual(1, wpf.SplitList.Count);
            Assert.AreEqual(hours, wpf.SplitList[0].Hours);
            Assert.AreEqual(minutes, wpf.SplitList[0].Minutes);
            Assert.AreEqual(seconds, wpf.SplitList[0].Seconds);
        }

        [TestMethod]
        public void CalculateTypeCommandTest()
        {
            WpfVM wpf = new WpfVM();

            Assert.IsFalse(wpf.CalculateTypeCommand.CanExecute(null));

            wpf.CalcPace = true;
            wpf.Time = "1:29:59";
            wpf.Distance = "13.1";

            Assert.IsTrue(wpf.CalculateTypeCommand.CanExecute(null));
        }

        [TestMethod()]
        public void ShowSplitsTest()
        {
            WpfVM wpf = new WpfVM();

            // set data
            wpf.Time = "5:00";
            wpf.Pace = "5:00";
            wpf.Distance = "1";
            wpf.DistanceType = "miles";
            wpf.PaceType = "minutesPerMile";
            wpf.SplitDistance = "0.25";
            wpf.SplitDistanceType = "miles";

            // run test
            wpf.ShowSplitsCommand.Execute(null);

            // validate test
            ObservableCollection<Split> splits = wpf.SplitList;

            Assert.AreEqual(0, splits[0].Hours);
            Assert.AreEqual(1, splits[0].Minutes);
            Assert.AreEqual(15, splits[0].Seconds);
            Assert.AreEqual(0.25, splits[0].Distance);
        }

        [TestMethod]
        public void ShowSplitsEnableTest()
        {
            WpfVM wpf = new WpfVM();

            Assert.IsFalse(wpf.ShowSplitsCommand.CanExecute(null));
            wpf.SplitDistance = "1";
            Assert.IsTrue(wpf.ShowSplitsCommand.CanExecute(null));
        }
    }
}