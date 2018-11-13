using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ViewModels.Tests
{
    [TestClass()]
    public class CreateStringsTests
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

        [DeploymentItem(@"../../UnitTestData/ConvertTimeToStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ConvertTimeToStringTest.csv",
                    "ConvertTimeToStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void ConvertTimeToStringTest()
        {
            // get data
            int hours = Convert.ToInt32(TestContext.DataRow["hours"]);
            int minutes = Convert.ToInt32(TestContext.DataRow["minutes"]);
            double seconds = Convert.ToDouble(TestContext.DataRow["seconds"]);
            string expected = Convert.ToString(TestContext.DataRow["expected"]);

            // run test
            string actual = CreateStrings.ConvertTimeToString(hours, minutes, seconds);

            // validate results
            Assert.AreEqual(expected, actual);
        }

        [DeploymentItem(@"../../UnitTestData/DistanceStringTest.csv"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\DistanceStringTest.csv",
                    "DistanceStringTest#csv",
                    DataAccessMethod.Sequential),
         TestMethod()]
        public void DistanceStringTest()
        {
            // get test data
            double expected = Convert.ToDouble(TestContext.DataRow["Distance"]);

            // run test
            string actual = CreateStrings.DistanceString(expected);

            // validate results
            Assert.AreEqual(expected.ToString("0.##"), actual);
        }
    }
}