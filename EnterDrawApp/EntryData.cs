using System;
using System.Collections.Generic;

namespace EnterDrawApp
{

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

        //For test purposes
        public EntryData()
        {
        }

    }
}