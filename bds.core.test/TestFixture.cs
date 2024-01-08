using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using bds.core;

namespace bds.test.core {
    [TestFixture]
    public class TestFixture1 {
        [Test]
        public void TestTrue()
        {
            Assert.IsTrue(true);
        }
        
        ASCIIEncoding encoder = new ASCIIEncoding();

        // This test fail for example, replace result or delete this test to see all tests pass
        [Test]
        public void TestFault() {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMD5Hasing() {
            bds.core.data.Security securityService = new bds.core.data.Security();
            TestClass obj1 = new TestClass();
            obj1.test1 = 1001;
            obj1.test2 = 9119;
            obj1.test3 = "abcdefg";

            TestClass obj2 = new TestClass();
            obj2.test1 = 1001;
            obj2.test2 = 9119;
            obj2.test3 = "abcdefg";

            var bytes = securityService.CreateMD5(obj1);
            var bytes2 = securityService.CreateMD5(obj2);
            Assert.AreEqual(bytes, bytes2);

            obj2.test2 = 9118;
            bytes = securityService.CreateMD5(obj1);
            bytes2 = securityService.CreateMD5(obj2);

            Assert.AreNotEqual(bytes, bytes2);

        }


        

    }

    [Serializable]
    public class TestClass {
        public int test1 { get; set; }
        public int test2 { get; set; }
        public string test3 { get; set; }

    }
}
