using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class ParseStringsTests
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

        [DeploymentItem(@"../../UnitTestData/ParseDistanceTypeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ParseDistanceTypeTest.csv",
                    "ParseDistanceTypeTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ParseDistanceTypeTest()
        {
            // create a Data object
            Data expectedData = new Data();
            Data.DistanceTypes actualDistanceType = expectedData.DistanceType;

            // get test data
            string distanceType = Convert.ToString(TestContext.DataRow["DistanceType"]);

            // determine the distanceType via helper methods below
            ConvertDistanceTypeString(expectedData, distanceType);

            // run test
            ParseStrings.ParseDistanceType(distanceType, ref actualDistanceType);

            // validate results
            Assert.AreEqual(expectedData.DistanceType, actualDistanceType, "Distance types differed.");
        }

        [DeploymentItem(@"../../UnitTestData/ParsePaceTypeTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ParsePaceTypeTest.csv",
                    "ParsePaceTypeTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ParsePaceTypeTest()
        {
            // create a Data object
            Data expectedData = new Data();
            Data.PaceTypes actualPaceType = expectedData.PaceType;

            // get test data
            string paceType = Convert.ToString(TestContext.DataRow["PaceType"]);

            // determine the distanceType via helper methods below
            ConvertPaceTypeString(expectedData, paceType);

            // run test
            ParseStrings.ParsePaceType(paceType, ref actualPaceType);

            // validate results
            Assert.AreEqual(expectedData.PaceType, actualPaceType, "Distance types differed.");
        }

        [DeploymentItem(@"../../UnitTestData/ParseTimeStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ParseTimeStringTest.csv",
                    "ParseTimeStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ParseTimeStringTest()
        {
            // get test data
            string timeString = Convert.ToString(TestContext.DataRow["timeString"]);
            int expectedHours = Convert.ToInt32(TestContext.DataRow["hours"]);
            int expectedMinutes = Convert.ToInt32(TestContext.DataRow["minutes"]);
            double expectedSeconds = Convert.ToDouble(TestContext.DataRow["seconds"]);
            Boolean expectedPass = Convert.ToBoolean(TestContext.DataRow["expectPass"]);

            int hours = -1;
            int minutes = -1;
            double seconds = -1;
            Boolean actualPass;

            // run test
            actualPass = ParseStrings.ParseTimeString(timeString, ref hours, ref minutes, ref seconds);

            // validate results
            Assert.AreEqual(expectedPass, actualPass);
            Assert.AreEqual(expectedHours, hours, "hours differed.");
            Assert.AreEqual(expectedMinutes, minutes, "minutes differed.");
            Assert.AreEqual(expectedSeconds, seconds, "seconds differed.");
        }

        [DeploymentItem(@"../../UnitTestData/ParseDistanceStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ParseDistanceStringTest.csv",
                    "ParseDistanceStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ParseDistanceStringTest()
        {
            // get test data
            string distanceString = Convert.ToString(TestContext.DataRow["distanceString"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["distance"]);

            double distance = -1;

            // run test
            ParseStrings.ParseDistanceString(distanceString, ref distance);

            // validate results
            Assert.AreEqual(expectedDistance, distance, "distance differed.");
        }

        [DeploymentItem(@"../../UnitTestData/ParseSplitDistanceStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ParseSplitDistanceStringTest.csv",
                    "ParseSplitDistanceStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ParseSplitDistanceStringTest()
        {
            // get test data
            string splitDistanceString = Convert.ToString(TestContext.DataRow["splitDistanceString"]);
            double expectedDistance = Convert.ToDouble(TestContext.DataRow["distance"]);

            double distance = -1;

            // run test
            ParseStrings.ParseSplitDistanceString(splitDistanceString, ref distance);

            // validate results
            Assert.AreEqual(expectedDistance, distance, "distance differed.");
        }

        // helper methods below here

        /// <summary>
        /// Turns a string representing a paceType enumeration into a paceType
        /// </summary>
        /// <param name="paceType"></param>
        public void ConvertPaceTypeString(Data data, string paceType)
        {
            switch (paceType)
            {
                case "minutesPerMile":
                    data.PaceType = Data.PaceTypes.minutesPerMile;
                    break;
                case "minutesPerKilometer":
                    data.PaceType = Data.PaceTypes.minutesPerKilometer;
                    break;
                default:
                    data.PaceType = Data.PaceTypes.minutesPerKilometer;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distanceType"></param>
        public void ConvertDistanceTypeString(Data data, string distanceType)
        {
            switch (distanceType)
            {
                case "miles":
                    data.DistanceType = Data.DistanceTypes.miles;
                    break;
                case "kilometers":
                    data.DistanceType = Data.DistanceTypes.kilometers;
                    break;
                default:
                    data.DistanceType = Data.DistanceTypes.kilometers;
                    break;
            }
        }
    }
}