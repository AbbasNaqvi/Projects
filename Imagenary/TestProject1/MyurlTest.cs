using Imagenary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for MyurlTest and is intended
    ///to contain all MyurlTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MyurlTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
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
        #endregion


        /// <summary>
        ///A test for Change_Angle
        ///</summary>
        [TestMethod()]
        public void Change_AngleTest()
        {
            //Myurl target = new Myurl(); // TODO: Initialize to an appropriate value
            //string url = "https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,182.03h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US"; // TODO: Initialize to an appropriate value
            //string angle = "90h"; // TODO: Initialize to an appropriate value
            //string expected ="https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,90h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US" ; // TODO: Initialize to an appropriate value
            //string actual;
            //actual = target.Change_Angle(url, angle,false,"");
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
