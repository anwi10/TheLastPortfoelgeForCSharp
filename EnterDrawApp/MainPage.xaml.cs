using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EnterDrawAppLib;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EnterDrawApp
{
    public sealed partial class MainPage : Page
    {
        //Summary
        //This class implements the main APP logic. 
        //It contains the data from app Entries, can validate it, and persist it to a file

        //Setup objects to hold entry data
        public EntryData objectThatHoldsAllEntryData;

        //Setup a object that holds, validate, and can create serial numbers.
        private SerialNumbers validSerialNumbers;

        //Object that can serialize an object to and from a file
        private DataPersistence dP;

        //Main function call when application is started
        public MainPage()
        {
           
            InitializeComponent();

            //Initialize serialization object
            dP = new DataPersistence();

            //Initialize Data object
            objectThatHoldsAllEntryData = new EntryData();

            //Initialize Serial number object
            validSerialNumbers = new SerialNumbers();

            //Get the state of the SerialNumbers from file, if file dont exist, create 100 valid serial numbers 
            if (File.Exists(dP.GetPath(DataPersistence.FileLokationMode.FileLocationForValidSerialNumbers)))
            {
                validSerialNumbers = (SerialNumbers) dP.DeserializeObject(validSerialNumbers, 
                    DataPersistence.FileLokationMode.FileLocationForValidSerialNumbers);  
            }
            else
            {
                validSerialNumbers.GenerateValidSerialNumbers(100);
            }

            //Get the state of the application, from persistet object, if it dont exist, do nothing.
            if (File.Exists(dP.GetPath(DataPersistence.FileLokationMode.FileLocationForPersonalEntryData)))
            {
                //deserialize object from file
                objectThatHoldsAllEntryData = (EntryData) dP.DeserializeObject(objectThatHoldsAllEntryData,
                    DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);
            }

            //Subscribe on the app suspending event. This is thrown when the app goes un focused or is shut down 
            //It is used for persisting the state og the application.
            Application.Current.Suspending += App_Suspending;

        }
    }
}
