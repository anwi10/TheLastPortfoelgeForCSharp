using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EnterDrawApp
{
    public sealed partial class MainPage : Page
    {
        private bool ValidateEntryData()
        {
            //Validate incomming info
            bool exceptionFlag = true;
            try
            {
                //Look through all previous entered data to check for dublications, aka, you cant use the same email / phone / serial, number twice
                foreach (var entryData in objectThatHoldsAllEntryData.listOfEntryData)
                {
                    if (serialNrTextBox.Text == entryData.serialNumber)
                    {
                        throw new SerialNrNotValidException(
                        "This serial number have already been used. ");
                    }
                    if (eMailTextBox.Text == entryData.eMail)
                    {
                        throw new EmailNotValidException("This E-mail have already been used. ");
                    }
                    if (phoneNrTextBox.Text == entryData.phoneNr)
                    {
                        throw new PhoneNrNotValidException("This phone number have already been used. ");
                    }
                }

                //Validate for type structure, aka an email should contain @, phonenumber should be 8 char, serial numbers shoudl start with #
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

        //Validate that the login attemp uses the correct data.
        private bool ValidateLoginAtempt()
        {
            bool validationFlag = true;

            try
            {
                if (userNameTextBox.Text != "Jon skeet")
                {
                    throw new LoginErrorEception("Invalid username");
                }
                if (passwordTextBox.Text != "991906") //Jon skeets Stackoverflow reputation
                {
                    throw new LoginErrorEception("Invalid password");
                }
            }
            catch (LoginErrorEception ex)
            {
                latest_1TextBlock.Text = ex.Message;
                validationFlag = false;
            }
            return validationFlag;
        }
    }
}
