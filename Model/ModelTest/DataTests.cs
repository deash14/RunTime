using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Model.Tests
{
    [TestClass()]
    public class DataTests
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

        [DeploymentItem(@"../../UnitTestData/ValidBaseDataTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ValidBaseDataTest.csv",
                    "ValidBaseDataTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ValidBaseDataTest()
        {
            // create a Data object
            Data data = new Data();

            // get test data
            data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            data.PaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            data.PaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            data.PaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);
            Boolean expectedResult = Convert.ToBoolean(TestContext.DataRow["result"]);

            // run test
            Boolean results = data.ValidBaseData();

            // validate results
            Assert.AreEqual(expectedResult, results, "Boolean results differed.");
        }

        [DeploymentItem(@"../../UnitTestData/ValidExtendedDataTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ValidExtendedDataTest.csv",
                    "ValidExtendedDataTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ValidExtendedDataTest()
        {
            // create a Data object
            Data data = new Data();

            // get test data
            data.Hours = Convert.ToInt32(TestContext.DataRow["Hours"]);
            data.Minutes = Convert.ToInt32(TestContext.DataRow["Minutes"]);
            data.Seconds = Convert.ToDouble(TestContext.DataRow["Seconds"]);
            data.Distance = Convert.ToDouble(TestContext.DataRow["Distance"]);
            data.PaceHours = Convert.ToInt32(TestContext.DataRow["PaceHours"]);
            data.PaceMinutes = Convert.ToInt32(TestContext.DataRow["PaceMinutes"]);
            data.PaceSeconds = Convert.ToDouble(TestContext.DataRow["PaceSeconds"]);
            data.SplitDistance = Convert.ToDouble(TestContext.DataRow["SplitDistance"]);
            Boolean expectedResult = Convert.ToBoolean(TestContext.DataRow["result"]);

            // run test
            Boolean results = data.ValidExtendedData();

            // validate results
            Assert.AreEqual(expectedResult, results, "Boolean results differed.");
        }
    }
}