using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using EnterDrawAppLib;


namespace EnterDrawApp
{
    public sealed partial class MainPage : Page
    {
        //This executes when the ap becomes suspended
        //Upon suspension the current state of the entry data, and serial numbers are persisted.
        private void App_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            //serialize Personal data
            dP.SerializeObject(objectThatHoldsAllEntryData, DataPersistence.FileLokationMode.FileLocationForPersonalEntryData);

            //serialize serial numbers
            dP.SerializeObject(validSerialNumbers,DataPersistence.FileLokationMode.FileLocationForValidSerialNumbers);
        }

        //Executes when text box get focus
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //Clear text box when it gets focus
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        //Executes when DoneButton is clicked
        //This is where all data in the text fields are validated. 
        //If all data is valid it is saved to the EntryData object for storage
        private async void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Clear display box
            //So new messages can be displayed
            displayBox.Text = "";

            //Validate entry data using exceptions
            if (await ValidateEntryData())
            {
                //If there was no errors in the data, save the data
                objectThatHoldsAllEntryData.SaveEntryToList
                (
                    firstNameTextBox.Text, 
                    surNameTextBox.Text, 
                    eMailTextBox.Text,
                    phoneNrTextBox.Text, 
                    dateOfBirthDatePicker.Date.Date, 
                    serialNrTextBox.Text
                );

                var serialNumber = serialNrTextBox.Text;

                //if valid data entered, check if this serial number is a win
                if (validSerialNumbers.ValidateSerialnumber(serialNumber))
                {
                    //Remove the serial number from the list
                    validSerialNumbers.RemoveSerialNumber(serialNrTextBox.Text);
                    
                    //Display succes
                    displayBox.Text = "Congratulation you won!";
                }
                else
                {
                    //Display if they lost
                    displayBox.Text = "Sorry, you didnt win this time";
                }
                
            }
        }

        //Executes when login button is clicked
        private void ShowLatestEntries_OnClick(object sender, RoutedEventArgs e)
        {
            //Validate the login attempt
            if (ValidateLoginAtempt())
            {
                //Place all tekst boxes in a list, so they can eb iterated over
                List<TextBlock> listOfBlocks = new List<TextBlock>()
                {   latest_1TextBlock,
                    latest_2TextBlock,
                    latest_3TextBlock,
                    latest_4TextBlock,
                    latest_5TextBlock,
                    latest_6TextBlock,
                    latest_7TextBlock,
                    latest_8TextBlock
                };

                try
                {
                    //Iterator so the list can ben indekst
                    int entryDataIterator = 1;

                    //Iterate over the list
                    foreach (var tekstBlock in listOfBlocks)
                    {
                        tekstBlock.Text = objectThatHoldsAllEntryData.
                            listOfEntryData[objectThatHoldsAllEntryData.listOfEntryData.Count - entryDataIterator].serialNumber;
                        entryDataIterator++;
                    }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    //If the call to the above functions goes out of range.
                }

            }
            
        }
    }
}
