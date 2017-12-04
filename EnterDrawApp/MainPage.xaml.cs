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
using FilePersistence;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EnterDrawApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Initialization of objects to hold entry data

        private EntryData eD;

        //Initialization of serialization object
        private DataPersistence dP;



        public MainPage()
        {
            this.InitializeComponent();
            Application.Current.Suspending += App_Suspending;

            //Initialize serialization object
            dP = new DataPersistence();

            //Get the state of the application, from persistet object
            if (File.Exists(dP.GetPath(DataPersistence.FileLokationMode.FileLocationForPersonalEntryData)))
            {

                eD = new EntryData();

                eD = (EntryData) dP.DeserializeObject(eD,
                    DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);

                //firstNameTextBox.Text = eD.listOfEntryData[0].firstName;
                //surNameTextBox.Text = eD.listOfEntryData[0].surName;
            } 
        }

        private async void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Clear display box
            DisplayBox.Text = "";

            Task<bool> validationTask = new Task<bool>(ValidateEntryData);
                 validationTask.Start();
            bool test = await validationTask;
                if (test)
                {
                    //Save the data
                    eD.SaveEntryToList(firstNameTextBox.Text,surNameTextBox.Text,eMailTextBox.Text,
                        phoneNrTextBox.Text,dateOfBirthDatePicker.Date.Date,serialNrTextBox.Text);

                    //Succes
                    DisplayBox.Text = "Data saved succesfully";
                }
        }

        void App_Suspending(
            object sender,
            Windows.ApplicationModel.SuspendingEventArgs e)
        {
            // TODO: This is the time to save app data in case the process is terminated

            //Serialize object
            if (eD != null)
            {
                dP.SerializeObject(eD, DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);
            }
            

        }

        private bool ValidateEntryData()
        {
            //Validate incomming info
            bool exceptionFlag = true;
            try
            {
                if (firstNameTextBox.Text.Length > 20)
                {
                    throw new NameNotValidException("first name should be below 20 character. Use initials. ");
                }
                if (!eMailTextBox.Text.Contains("@"))
                {
                    throw new EmailNotValidException("Email invalid, valid emails should contain \"@\". ");
                }
                if (phoneNrTextBox.Text.Length != 8)
                {
                    throw new PhoneNrNotValidException("Phone number invalid, phone number should be 8 characters. ");
                }
                if (!serialNrTextBox.Text.StartsWith("#"))
                {
                    throw new SerialNrNotValidException(
                        "Serial number invalid, serial numbers should start with a \"#\". ");
                }
            }
            catch (NameNotValidException ex)
            {
                DisplayBox.Text += ex.Message;
                exceptionFlag = false;
            }
            catch (EmailNotValidException ex)
            {
                DisplayBox.Text += ex.Message;
                exceptionFlag = false;
            }
            catch (PhoneNrNotValidException ex)
            {
                DisplayBox.Text += ex.Message;
                exceptionFlag = false;
            }
            catch (SerialNrNotValidException ex)
            {
                DisplayBox.Text += ex.Message;
                exceptionFlag = false;
            }
            return exceptionFlag;

        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
    }
}
