using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ViewModels.Tests
{
    [TestClass()]
    public class DelegateCommandTests
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

        public void TestFunc()
        {

        }

        public bool TestCanExecuteFalse()
        {
            return false;
        }

        public bool TestCanExecuteTrue()
        {
            return true;
        }

        // end helper methods

        [TestMethod()]
        public void CanExecuteTest()
        {
            DelegateCommand dc = new DelegateCommand(TestFunc, null);

            // when no canExecute method (in constructor) is give should return true.
            Assert.IsTrue(dc.CanExecute(null));

            dc = new DelegateCommand(TestFunc, TestCanExecuteFalse);
            Assert.IsFalse(dc.CanExecute(null));

            dc = new DelegateCommand(TestFunc, TestCanExecuteTrue);
            Assert.IsTrue(dc.CanExecute(null));
        }

        [TestMethod]
        public void RaiseCanExecuteChangedTest()
        {
            DelegateCommand dc = new DelegateCommand(TestFunc);
            dc.RaiseCanExecuteChanged();

            Assert.IsTrue(dc.CanExecute(null));

            dc = new DelegateCommand(TestFunc, TestCanExecuteTrue);
            dc.RaiseCanExecuteChanged();

            Assert.IsTrue(dc.CanExecute(null));
        }
    }
}