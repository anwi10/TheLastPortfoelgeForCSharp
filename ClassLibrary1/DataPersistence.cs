using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Windows.Data.Text;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Microsoft.Win32.SafeHandles;
using NUnit.Framework;
using System.Collections.Generic;

namespace FilePersistence
{
    public class DataPersistence
    {
        //Constant storage name
        private const string FileLocationForPersonalEntryData = "PersonalData.txt";

        private const string FileLocationForUsedSerialNumbers = "UsedSerialNumbers.txt";

        private const string FileLocationForValidSerialNumbers = "ValiedSerialNumbers.txt";

        //Initialization of a struct that holds all the information from the current entry
        private EntryData structToHoldAllTheEntryData;

        public List<string> ListstoringReadPersonalData;

        private uint SizeOFLastWriteStream;
        //Struct to hold the data of an entry in the app
        public struct EntryData
        {
            public string firstName;
            public string surName;
            public string eMail;
            public string phoneNr;
            public DateTime date;
            public string serialNumber;

            public EntryData(string fN, string sN, string eM, string pNr, DateTime dT, string sNr)
            {
                firstName = fN;
                surName = sN;
                eMail = eM;
                phoneNr = pNr;
                date = dT;
                serialNumber = sNr;
            }
            public EntryData(string fN)
            {
                firstName = fN;
                surName = "0";
                eMail = "0";
                phoneNr = "0";
                date = DateTime.MaxValue;
                serialNumber = "0";
            }

        }
       

        public DataPersistence(string firstName, string surName, string eMail,string phoneNr, DateTime date, string serialNumber)
        {
            structToHoldAllTheEntryData = new EntryData(firstName, surName, eMail, phoneNr, date, serialNumber);
            SizeOFLastWriteStream = 0;
        }

        public async Task SaveUserInformation()
        {
            FileStream fs = null;
            string testdata = "";
            try
            {

                // Create file; replace if exists.
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

                StorageFile FileForPersonalEntryData =
                    await storageFolder.CreateFileAsync(FileLocationForPersonalEntryData,
                        CreationCollisionOption.OpenIfExists);
//                var stream = await FileForPersonalEntryData.OpenAsync(FileAccessMode.ReadWrite);
//                ulong size = stream.Size;
                fs = new FileStream(FileForPersonalEntryData.Path,FileMode.OpenOrCreate);
                //SizeOFLastWriteStream = (uint)size;
                /* using (var outputStream = stream.GetOutputStreamAt((uint)size))
                 {*/
                using (var streamwriter = new StreamWriter(fs,Encoding.UTF8))
                {
                    await streamwriter.WriteLineAsync(structToHoldAllTheEntryData.firstName);
                    await streamwriter.WriteLineAsync(structToHoldAllTheEntryData.firstName);
                    await streamwriter.WriteLineAsync(structToHoldAllTheEntryData.surName);
                    await streamwriter.WriteLineAsync(structToHoldAllTheEntryData.eMail);
                    await streamwriter.WriteLineAsync(structToHoldAllTheEntryData.phoneNr);
                    await streamwriter.WriteLineAsync(structToHoldAllTheEntryData.date.ToString());
                    await streamwriter.WriteLineAsync("|");

                }
                //}
                if (fs != null)
                    fs.Dispose();
            }
            finally
            {

            }
            /*
            //Try/finally to close the stream
            try
            {
                // Create file; replace if exists.
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile FileForPersonalEntryData =
                    await storageFolder.CreateFileAsync(FileLocationForPersonalEntryData,CreationCollisionOption.OpenIfExists);
                    //FileLocationForPersonalEntryData, CreationCollisionOption.OpenIfExists
                fs = new FileStream(FileForPersonalEntryData.Path,FileMode.Open);

                using (StreamWriter dataWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    dataWriter.Write(structToHoldAllTheEntryData.firstName);
                    dataWriter.Write(structToHoldAllTheEntryData.surName);
                    dataWriter.Write(structToHoldAllTheEntryData.eMail);
                    dataWriter.Write(structToHoldAllTheEntryData.phoneNr);
                    dataWriter.Write(structToHoldAllTheEntryData.date.ToString());
                }
                
            }
            finally
            {
                if(fs != null)
                fs.Dispose();
            }*/
        }
        
        public void simplefunction()
        {
            
        }

        public async Task GetUserInformation()
        {
            ListstoringReadPersonalData = new List<string>();
            FileStream fs = null;
            string testdata = "";
            try
            {
                
                // Create file; replace if exists.
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
               
                StorageFile FileForPersonalEntryData =
                    await storageFolder.CreateFileAsync(FileLocationForPersonalEntryData,
                        CreationCollisionOption.OpenIfExists);
                fs = new FileStream(FileForPersonalEntryData.Path,FileMode.OpenOrCreate);
                    using (var streamreader = new StreamReader(fs))
                    {
                        while (testdata != "|")
                        {
                            ListstoringReadPersonalData.Add(testdata);
                            testdata = streamreader.ReadLine();
                    }
                    }
 

                if (fs != null)
                    fs.Dispose();
                
            }
            finally
            {
                
            }

        }
    }
}