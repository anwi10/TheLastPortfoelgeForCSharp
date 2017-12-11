using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EnterDrawApp
{
    [DataContract]
    public class EntryData
    {
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


        public void SaveEntryToList(string fN, string sN, string eM, string pNr, DateTime dT, string sNr)
        {
            DataHolder LocalInstanceOfDataHolder = new DataHolder();
            LocalInstanceOfDataHolder.firstName = fN;
            LocalInstanceOfDataHolder.surName = sN;
            LocalInstanceOfDataHolder.eMail = eM;
            LocalInstanceOfDataHolder.phoneNr = pNr;
            LocalInstanceOfDataHolder.date = dT;
            LocalInstanceOfDataHolder.serialNumber = sNr;
            listOfEntryData.Add(LocalInstanceOfDataHolder);
        }

        public EntryData()
        {
        }

        //Overload for unit test compare
        public override bool Equals(Object obj)
        {
            if (obj is EntryData)
            {
                EntryData that = obj as EntryData;

                for (int i = 0; i < listOfEntryData.Count; i++)
                {
                    if(listOfEntryData[i].serialNumber != that.listOfEntryData[i].serialNumber 
                        | listOfEntryData[i].firstName != that.listOfEntryData[i].firstName 
                        | listOfEntryData[i].surName != that.listOfEntryData[i].surName
                        | listOfEntryData[i].eMail != that.listOfEntryData[i].eMail 
                        | listOfEntryData[i].phoneNr != that.listOfEntryData[i].phoneNr
                        | listOfEntryData[i].date != that.listOfEntryData[i].date)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}