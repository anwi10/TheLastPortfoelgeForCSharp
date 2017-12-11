using System; 
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilePersistence;


namespace EnterDrawApp
{
    [DataContract]
    class SerialNumbers
    {
        [DataMember]
        public List<string> validSerialNumbers = new List<string>();

        public SerialNumbers()
        {
        }

        public void GenerateValidSerialNumbers(int amountOfNumbers)
        {
            validSerialNumbers.Add("#1");
            validSerialNumbers.Add("#2");
            validSerialNumbers.Add("#3");
            Random rnd = new Random();

            for (int i = 0; i < amountOfNumbers; i++)
            {
                int number = rnd.Next(1000, 100000); //The chance of duplet, when under 100 amounts, is very small.
                validSerialNumbers.Add("#" + number.ToString());
            }
        }

        public bool ValidateSerialnumber(string serialNumber)
        {
            return validSerialNumbers.Contains(serialNumber);
        }

        public void RemoveSerialNumber(string serialNumber)
        {
            validSerialNumbers.Remove(serialNumber);
        }

    }
}
