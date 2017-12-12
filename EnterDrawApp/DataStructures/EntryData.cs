using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EnterDrawApp
{
    //Summary
    //This class can hold Entry information, and compare objects of this type

    [DataContract] //Data contract indicates that this class is serializable
    public class EntryData
    {
        //Struct to groupe all entry data (Potentialy bad practice in C#)
        public struct DataHolder
        {
            public string firstName;
            public string surName;
            public string eMail;
            public string phoneNr;
            public DateTime date;
            public string serialNumber;
        }

        [DataMember]
        public List<DataHolder> listOfEntryData = new List<DataHolder>();

        //Empty constructor
        public EntryData()
        {
        }

        //Save the incomming data to the list
        public void SaveEntryToList(string fN, string sN, string eM, string pNr, DateTime dT, string sNr)
        {
            //Create instance of struct
            DataHolder LocalInstanceOfDataHolder = new DataHolder
            {
                firstName = fN,
                surName = sN,
                eMail = eM,
                phoneNr = pNr,
                date = dT,
                serialNumber = sNr
            };
            //Push to the end of the list
            listOfEntryData.Add(LocalInstanceOfDataHolder);
        }

        //Overload for unit test "Equals"
        public override bool Equals(Object obj)
        {
            //If object is of right type
            if (obj is EntryData)
            {
                //Make a cast
                EntryData that = obj as EntryData;

                //Compare all ellements
                for (int i = 0; i < listOfEntryData.Count; i++)
                {
                    if(listOfEntryData[i].serialNumber != that.listOfEntryData[i].serialNumber 
                        | listOfEntryData[i].firstName != that.listOfEntryData[i].firstName 
                        | listOfEntryData[i].surName != that.listOfEntryData[i].surName
                        | listOfEntryData[i].eMail != that.listOfEntryData[i].eMail 
                        | listOfEntryData[i].phoneNr != that.listOfEntryData[i].phoneNr
                        | listOfEntryData[i].date != that.listOfEntryData[i].date)
                    {
                        //If one ellement was not equal
                        return false;
                    }
                }
                //If no comparison was false
                return true;
            }
            //If the object was not of the exspected type
            return false;
        }

        //Apparently you need to override GetHashCode together with Equals...
        //Deafault generatet function
        public override int GetHashCode()
        {
            return 1236339184 + EqualityComparer<List<DataHolder>>.Default.GetHashCode(listOfEntryData);
        }
    }
}