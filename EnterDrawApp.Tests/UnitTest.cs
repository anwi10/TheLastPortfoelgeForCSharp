using System;
using DataPersistenceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using EnterDrawApp;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Windows.UI.Xaml.Documents;

namespace EnterDrawApp.Tests
{
    //For testing serialization of an object
    [TestClass]
    public class LibraryTests
    {
        //Test that the serialization of an object to the file system
        //And test for succes full recovery
        [TestMethod]
        public void SerializationTest()
        {
            //Set-up
            EntryData Serializedata = new EntryData();
            EntryData deserializeData = new EntryData();
            DataPersistence dataPersistence = new DataPersistence();

            //Setup of entry data
            DateTime testDate = new DateTime(2017, 11, 30);
            string testName = "Anders";
            string testSurName = "Winter";
            string testEMail = "awint15@student.sdu.dk";
            string testPhoneNR = "51297085";
            string validSerialNumber = "#Valid";
            

            //Get current state of EntryData object, if there is one
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

    //Test that the method "ValidateEntryData" works for different data. 
    [TestClass]
    public class DataValidationTest
    {
        //Test the method with a valid set of data entries. 
        //Expect the method to return true on this set of data
        [TestMethod]
        public async Task ValidDataStructureTest()
        {
            //Run the code in the current application context, using a Dispatcher to run it Async. 
           await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
             {
                 
                 MainPage main = new MainPage
                 {
                     //Setup valid information
                     FirstName = "TestName",
                     SerialNumber = "#UnitTestSerialNumber#",
                     SurName = "TestsurName",
                     Email = "UnitTestMail@UnitTest.com",
                     PhoneNumber = "00000000"
                 };

                 Assert.IsTrue(await main.ValidateEntryData());
             });
        }

        //Test for a draw entry with an invalid Serial number
        [TestMethod]
        public async Task InvalidSerialNumberTest()
        {
            //Run the code in the current application context, using a Dispatcher to run it Async.
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                MainPage main = new MainPage
                {

                    //Setup valid information
                    FirstName = "TestName",
                    SerialNumber = "UnitTestInvalidSerialNumber",
                    SurName = "TestsurName",
                    Email = "UnitTestMail@UnitTest.com",
                    PhoneNumber = "00000000"
                };

                //The expected text in the display box 
                string unitTestPassCondition = "Serial number invalid, serial numbers should start with a \"#\". ";

                Assert.IsTrue(!await main.ValidateEntryData() && main.DisplayBox == unitTestPassCondition);
            });
        }

        //Test for a draw entry with invalid E-mail
        [TestMethod]
        public async Task InvalidEmailTest()
        {
            //Run the code in the current application context, using a Dispatcher to run it Async.
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                MainPage main = new MainPage
                {

                    //Setup valid information
                    FirstName = "TestName",
                    SerialNumber = "#UnitTestSerialNumber#",
                    SurName = "TestsurName",
                    Email = "InvalidUnitTestEmail",
                    PhoneNumber = "00000000"
                };

                //The expected text in the display box
                string unitTestPassCondition = "Email invalid, valid emails should contain \"@\". ";

                Assert.IsTrue(!await main.ValidateEntryData() && main.DisplayBox == unitTestPassCondition);
            });
        }

        //Test for a draw entry with invalid phone number
        [TestMethod]
        public async Task InvalidPhoneNumberTest()
        {
            //Run the code in the current application context, using a Dispatcher to run it Async.
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                MainPage main = new MainPage
                {

                    //Setup valid information
                    FirstName = "TestName",
                    SerialNumber = "#UnitTestSerialNumber#",
                    SurName = "TestsurName",
                    Email = "UnitTestMail@UnitTest.com",
                    //Invalid phone nr lenght (7 char)
                    PhoneNumber = "0000000"
                };

                //Expected text in the display textbox
                string unitTestPassCondition = "Phone number invalid, phone number should be 8 characters. ";

                Assert.IsTrue(!await main.ValidateEntryData() && main.DisplayBox == unitTestPassCondition);
            });
        }
    }
} 
