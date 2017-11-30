using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Microsoft.Win32.SafeHandles;


namespace FilePersistence
{
    public class DataPersistence
    {
        //Constant storage name
        private const string storageName = "MyTest.txt";

        //Persistence data
        private string firstName;
        private string surName;
        private string eMail;
        private string phoneNr;
        private DateTime date;
        private string serialNumber;

        public DataPersistence(string firstName, string surName, string eMail,string phoneNr, DateTime date, string serialNumber)
        {
            this.firstName = firstName;
            this.surName = surName;
            this.date = date;
            this.eMail = eMail;
            this.phoneNr = phoneNr;
            this.date = date;
            this.serialNumber = serialNumber;

        }

        public async void SaveUserInformation()
        {
            //Using a filestream
            FileStream fs = null;
            //Try/finally to close the stream
            try
            {
                // Create file; replace if exists.
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile UserPersistenceFile = await storageFolder.CreateFileAsync("UserPersistence.txt",Windows.Storage.CreationCollisionOption.OpenIfExists);
                fs = new FileStream(UserPersistenceFile.Path,FileMode.Open);
                await FileIO.WriteTextAsync(UserPersistenceFile,"Hellow");
                
                using (StreamWriter dataWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    dataWriter.Write(firstName);
                    dataWriter.Write(surName);
                    dataWriter.Write(eMail);
                    dataWriter.Write(phoneNr);
                    dataWriter.Write(date.ToString());
                    dataWriter.Write(serialNumber);
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }
    }

    //Make a Unit Test here

}