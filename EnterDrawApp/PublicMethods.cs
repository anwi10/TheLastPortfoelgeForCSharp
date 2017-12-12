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

namespace EnterDrawApp
{
    //Get and setter methods from accesing the text boxes outside the MainPage class. 
    //Mostly used in the unit test

    public sealed partial class MainPage : Page
    {

        public string FirstName
        {
            get { return firstNameTextBox.Text; }
            set { firstNameTextBox.Text = value; }
        }

        public string SurName
        {
            get { return surNameTextBox.Text; }
            set { surNameTextBox.Text = value; }
        }

        public string Email
        {
            get { return eMailTextBox.Text; }
            set { eMailTextBox.Text = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNrTextBox.Text; }
            set { phoneNrTextBox.Text = value; }
        }

        public string SerialNumber
        {
            get { return serialNrTextBox.Text; }
            set { serialNrTextBox.Text = value; }
        }

        public DateTime DateAndtime
        {
            get { return dateOfBirthDatePicker.Date.Date; }
        }

        public string DisplayBox
        {
            get { return displayBox.Text; }
            set { displayBox.Text = value; }
        }
    }
}