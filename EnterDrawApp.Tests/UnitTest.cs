using System;
using FilePersistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EnterDrawApp.Tests
{
    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void SerializationTest()
        { 
            //Set-up
            DateTime testDate = new DateTime(2017, 11, 30);
            string testName = "Anders";
            String testSurName = "Winter";
            string testEMail = "awint15@studnet.sdu.dk";
            string testPhoneNR = "51297085";
            string valiedSerialNumber = "Valid";
            EntryData deserializeData = new EntryData();
            EntryData eD = new EntryData();
            eD.SaveEntryToList(testName, testSurName, testEMail, testPhoneNR, testDate, valiedSerialNumber);
            DataPersistence dP = new DataPersistence();

            //Serialize
            dP.SerializeObject(eD,DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);

            //Deserialize
            deserializeData = (EntryData) dP.DeserializeObject(deserializeData,
                DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);

            //Test if equal
            Assert.AreEqual(eD, deserializeData);
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
