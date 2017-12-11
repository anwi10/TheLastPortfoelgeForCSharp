using System;
using FilePersistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace EnterDrawApp.Tests
{
    [TestClass]
    public class LibraryTests
    {

        //Test that the serialization of an object to the file system
        //And test for suzzes full recovery
        [TestMethod]
        public void SerializationTest()
        {
            //Set-up
            EntryData Serializedata = new EntryData();
            EntryData deserializeData = new EntryData();
            DataPersistence dataPersistence = new DataPersistence();

            DateTime testDate = new DateTime(2017, 11, 30);
            string testName = "Anders";
            string testSurName = "Winter";
            string testEMail = "awint15@student.sdu.dk";
            string testPhoneNR = "51297085";
            string validSerialNumber = "#Valid";
            

            //Get current state of file object
            if (File.Exists(dataPersistence.GetPath(DataPersistence.FileLokationMode.FileLocationForPersonalEntryData)))
            {
                //deserialize object from file
                Serializedata = (EntryData)dataPersistence.DeserializeObject(Serializedata,
                    DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);
            }

            //Alter the current state
            Serializedata.SaveEntryToList(testName, testSurName, testEMail, testPhoneNR, testDate, validSerialNumber);

            //Serialize
            dataPersistence.SerializeObject(Serializedata,DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);

            //Deserialize
            deserializeData = (EntryData) dataPersistence.DeserializeObject(deserializeData,
                DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);

            //Test if equal
            Assert.AreEqual(Serializedata, deserializeData);
        }
    }

    [TestClass]
    public class DataValidationTest
    {
        [TestMethod]
        public void DataStructureTest()
        {

            MainPage main = new MainPage();


            Assert.IsNotNull(firstname);
        }

        [TestMethod]
        public void RepeatedEmailTest()
        {
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void RepeatedPhoneNrTest()
        {
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void RepeatedSerialNumberTest()
        {
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void ValidLoginAtemptTest()
        {
            Assert.IsFalse(true);
        }

    } 

    [TestClass]
    public class DisplayTest
    {
        [TestMethod]
        public void Display8LatestSerialNumbersTest()
        {
            Assert.IsFalse(true);
        }


    }

}
