using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FilePersistence
{
    public class Class1
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

        public Class1(string firstName, string surName, string eMail, string phoneNr, DateTime date, string serialNumber)
        {
            this.firstName = firstName;
            this.surName = surName;
            this.date = date;
            this.eMail = eMail;
            this.phoneNr = phoneNr;
            this.date = date;
            this.serialNumber = serialNumber;

        }

        public void SaveUserInformation()
        {
            //Using a filestream
            FileStream fs = null;
            //Try/finally to close the stream
            try
            {
                // Create file; replace if exists.
                fs = new FileStream(storageName, FileMode.OpenOrCreate);
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
}
