using System; 
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace EnterDrawApp
{
    //Summary
    //This class can hold, validate, and generate serial numbers

    [DataContract] //Indicated that this call is serializable
    class SerialNumbers
    {
        //List of serial numbers
        [DataMember]
        public List<string> validSerialNumbers = new List<string>();

        //Empty constructor
        public SerialNumbers()
        {
        }

        public void GenerateValidSerialNumbers(int amountOfNumbers)
        {

            //These two should always be in the list of valid numbers, due to unit tests
            validSerialNumbers.Add("#UnitTestSerialNumber#");
            validSerialNumbers.Add("#UnitTestSerialNumber1#");

            //Initialize a Random object, this is used forgenerating random serial numbers
            Random rnd = new Random();

            //Generate x amount of serial numbers
            for (int i = 0; i < amountOfNumbers; i++)
            {
                int number = rnd.Next(1000, 100000); //The chance of duplet, when generating under 100 numbers, is very small.
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
