using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Business.SysHelper.AddLog("2", "2");
            //DataBase.Class1<object>.Add(new object());
            //Assert.IsTrue(true);
        }
    }
}
