using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Model.Tests
{
    [TestClass()]
    public class CreateStringsTests
    {
        string pace = string.Empty;
        string time = string.Empty;
        string distance = string.Empty;
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

        [DeploymentItem(@"../../UnitTestData/PaceStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\PaceStringTest.csv",
                    "PaceStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void PaceStringTest()
        {
            //// create a Data & CreateString objects
            //Data data = new Model.Data();
            //CreateStrings createStrings = new CreateStrings(data);

            //// create event listener
            //CreateStrings.OnPaceChange += new CreateStrings.PaceChangeHandler(OnPaceChange);

            //// get test data
            //data.PaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            //data.PaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            //data.PaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);
            //string expectedPaceString = Convert.ToString(TestContext.DataRow["PaceString"]);

            //// run test
            //createStrings.PaceString();

            //// validate results
            //Assert.AreEqual(expectedPaceString, pace, "pace strings differed.");
        }

        [DeploymentItem(@"../../UnitTestData/TimeStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\TimeStringTest.csv",
                    "TimeStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void TimeStringTest()
        {
            //// create a Data & CreateString objects
            //Data data = new Model.Data();
            //CreateStrings createStrings = new CreateStrings(data);

            //// create event listener
            //CreateStrings.OnTimeChange += new CreateStrings.TimeChangeHandler(OnTimeChange);

            //// get test data
            //data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            //data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            //data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            //string expectedTimeString = Convert.ToString(TestContext.DataRow["TimeString"]);

            //// run test
            //createStrings.TimeString();

            //// validate results
            //Assert.AreEqual(expectedTimeString, time, "time strings differed.");
        }

        [DeploymentItem(@"../../UnitTestData/DistanceStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\DistanceStringTest.csv",
                    "DistanceStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void DistanceStringTest()
        {
            //// create a Data & CreateString objects
            //Data data = new Model.Data();
            //CreateStrings createStrings = new CreateStrings(data);

            //// create event listener
            //CreateStrings.OnDistanceChange += new CreateStrings.DistanceChangeHandler(OnDistanceChange);
           
            //// get test data
            //data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            //string expectedDistanceString = Convert.ToString(TestContext.DataRow["DistanceString"]);

            //// run test
            //createStrings.DistanceString();

            //// validate results
            //Assert.AreEqual(expectedDistanceString, distance, "distance strings differed.");
        }

        // helper method ... listeners for the events created in CreateStrings.cs

        /// <summary>
        /// Picks up pace changes from the model and sends them to the view.
        /// </summary>
        /// <param name="pace"></param>
        public void OnPaceChange(object obj, StringEventArgs paceEvent)
        {
            pace = paceEvent.Message;
        }

        /// <summary>
        /// Picks up pace changes from the model and sends them to the view.
        /// </summary>
        /// <param name="pace"></param>
        public void OnTimeChange(object obj, StringEventArgs timeEvent)
        {
            time = timeEvent.Message;
        }

        /// <summary>
        /// Picks up pace changes from the model and sends them to the view.
        /// </summary>
        /// <param name="pace"></param>
        public void OnDistanceChange(object obj, StringEventArgs distanceEvent)
        {
            distance = distanceEvent.Message;
        }
    }
}