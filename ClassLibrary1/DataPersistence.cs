using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace ClassLibraryDrawApp
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
        asda
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

        public async void SaveInformation()
        {
            //Using a filestream
            FileStream fs = null;
            FileInfo fi = null;
            //Try/finally to close the stream
            try
            {
                // Create a reference to a file.

                //fi = new FileInfo(@"c:\tmp\MyTest.txt");
                // Actually create the file.
                

               await Task.Run(() => fs = new FileStream(storageName,FileMode.OpenOrCreate,FileAccess.ReadWrite));
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
                    fi.Delete();
                }
            }
        }
    }
}