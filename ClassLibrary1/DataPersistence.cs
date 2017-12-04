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
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;

namespace FilePersistence
{
    public class DataPersistence
    {
        //Constant storage name
        public struct FileLokationMode
        {
            public const string FileLocationForPersonalEntryData = "PersonalData.txt";

            public const string FileLocationForUsedSerialNumbers = "UsedSerialNumbers.txt";

            public const string FileLocationForValidSerialNumbers = "ValiedSerialNumbers.txt";
        }

        //Stream set-up
        private Stream stream = null;

        //Serialize set-up
        private DataContractSerializer dcSerializer = null;

        //Directory set-up
        private StorageFolder storageFolder = null;


        //Methods
        public DataPersistence()
        {
            // Find app local folder
            storageFolder = ApplicationData.Current.LocalFolder;
        }
        

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
        
        //takes an object as parameter to know the type of object to deserialize
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

        public string GetPath(string fileLocationMode)
        {
            return storageFolder.Path + "\\" + fileLocationMode;
        }
    }
}