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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //Setup objects to hold entry data
        private EntryData objectThatHoldsAllEntryData;

        //Setup a SerialNumber object
        private SerialNumbers validSerialNumbers;

        //Setup of serialization object
        private DataPersistence dP;

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

            //Get the state of the application, from persistet object
            if (File.Exists(dP.GetPath(DataPersistence.FileLokationMode.FileLocationForPersonalEntryData)))
            {
                //deserialize object from file
                objectThatHoldsAllEntryData = (EntryData) dP.DeserializeObject(objectThatHoldsAllEntryData,
                    DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);
            }

            Application.Current.Suspending += App_Suspending;

        }
    }
}
