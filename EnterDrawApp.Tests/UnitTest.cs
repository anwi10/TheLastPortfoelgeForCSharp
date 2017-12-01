using System;
using FilePersistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EnterDrawApp.Tests
{
    [TestClass]
    public class LibraryTests
    {
        private DataPersistence testData;
       
           
        

        [TestMethod]
        public void LibraryTest1()
        {
            DateTime testDate = new DateTime(2017, 11, 30);
            string testName = "Anders";
            String testSurName = "Winter";
            string testEMail = "awint15@studnet.sdu.dk";
            string testPhoneNR = "51297085";
            string inValidSerialNumber = "InValid";
            string valiedSerialNumber = "Valid";
            testData = new DataPersistence(testName, testSurName, testEMail, testPhoneNR, testDate, valiedSerialNumber);
            testData.SaveUserInformation();

            Assert.AreEqual(testName, testData.GetUserInformation().Result.firstName);
        }
    }

    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void AppTest1()
        {
            Assert.IsFalse(true);
        }


    }
}
