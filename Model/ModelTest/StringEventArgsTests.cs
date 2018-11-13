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
    public class StringEventArgsTests
    {
        [TestMethod()]
        public void StringEventArgsTest()
        {
            string message = "jat";
           StringEventArgs sea = new StringEventArgs(message);

            Assert.AreEqual(message, sea.Message);
        }
    }
}