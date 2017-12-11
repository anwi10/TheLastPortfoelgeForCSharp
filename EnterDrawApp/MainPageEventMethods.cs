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
        //Executes on app suspension
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
        private void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Clear display box
            DisplayBox.Text = "";

            //NOTE: Cant validate data async due to exceptions...

            //Validate entry data using exceptions
            if (ValidateEntryData())
            {
                //Save the data
                objectThatHoldsAllEntryData.SaveEntryToList(firstNameTextBox.Text, surNameTextBox.Text, eMailTextBox.Text,
                    phoneNrTextBox.Text, dateOfBirthDatePicker.Date.Date, serialNrTextBox.Text);

                var serialNumber = serialNrTextBox.Text;
                //if valid data entered, check if this serial number is a win

                if (validSerialNumbers.ValidateSerialnumber(serialNumber))
                {
                    //Remove the serial number from the list
                    validSerialNumbers.RemoveSerialNumber(serialNrTextBox.Text);
                    
                    //Display succes
                    DisplayBox.Text = "Congratulation you won!";
                }
                else
                {
                    DisplayBox.Text = "Sorry, you didnt win this time";
                }
                
            }
        }

        private void ShowLatestEntries_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidateLoginAtempt())
            {
                //Place all boxes in a list
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
                    int entryDataIterator = 1;
                    foreach (var tekstBlock in listOfBlocks)
                    {
                        tekstBlock.Text = objectThatHoldsAllEntryData.listOfEntryData[objectThatHoldsAllEntryData.listOfEntryData.Count - entryDataIterator].serialNumber;
                        entryDataIterator++;
                    }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    //--
                }

            }
            
        }
    }
}
