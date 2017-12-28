using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Windows.Data.Text;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;

namespace DataPersistenceLib
{
    public class DataPersistence
    {
        //Constant storage names, used to indicate the two file locations for persistence
        public struct FileLokationMode
        {
            public const string FileLocationForPersonalEntryData = "PersonalData.txt";
            public const string FileLocationForValidSerialNumbers = "ValidSerialNumbers.txt";
        }

        //Stream set-up
        private Stream stream = null;

        //Serialize set-up
        private DataContractSerializer dcSerializer = null;

        //Directory set-up
        private StorageFolder storageFolder = null;


        //Constructor finds this applications Local folder
        //On my system it goes: AppData\Local\Packages\5ec0059e-99e0-4e01-a18e-ba7b5592dff6_pth98vj66mbec\LocalState 
        public DataPersistence()
        {
            // Find app local folder
            storageFolder = ApplicationData.Current.LocalFolder;
        }
        
        //Serializes an ovject onto the choosen fileLocationMode
        public void SerializeObject(object objectToSerialize, string fileLocationMode)
        {

            //Use the app local folder + filename to point at file
            stream = File.Open(storageFolder.Path + "\\" + fileLocationMode, FileMode.OpenOrCreate);

            //Define the type of object to serialize / initialize the serializer
            dcSerializer = new DataContractSerializer(objectToSerialize.GetType());

            try
            {
                //Serialize the object
                dcSerializer.WriteObject(stream, objectToSerialize);
            }
            finally
            {
                //Close the stream, if open
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
        }
        
        //Deserializes an object of the type passed to the function
        public object DeserializeObject(object objectType, string fileLocationMode)
        {
            //Use the app local folder + filename to point at file
            stream = File.Open(storageFolder.Path + "\\" + fileLocationMode, FileMode.OpenOrCreate);

            //Define the type of object to serialize / initialize the serializer
            dcSerializer = new DataContractSerializer(objectType.GetType());

            //Object to hold return information
            object objectToDeserialize = null;

            try
            {
                //Untill the stream ends
                while (stream.CanRead)
                {
                    //Read in the object
                    objectToDeserialize = dcSerializer.ReadObject(stream);
                }
            } //in chase, for some reason the function tryes to read after the end of the stream 
            catch (System.Xml.XmlException)
            {
                return objectToDeserialize;
            }
            finally
            { 
                //Close the stream, if open
                if (stream != null)
                {
                    stream.Dispose();
                }   
            }
            return objectToDeserialize;
        }

        //Returns the path to the applications local folder
        public string GetPath(string fileLocationMode)
        {
            return storageFolder.Path + "\\" + fileLocationMode;
        }
    }
}