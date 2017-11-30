using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        public MainPage()
        {
            this.InitializeComponent();

        }

        private async void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            string fN = firstNameTextBox.Text;
            string sN = surNameTextBox.Text;
            string eM = eMailTextBox.Text;
            string pN = phoneNrTextBox.Text;
            DateTime dB = dateOfBirthDatePicker.Date.DateTime;
            string serialN = serialNrTextBox.Text;

            DataPersistence datePer = new  DataPersistence(fN,sN,eM,pN,dB,serialN);

            await Task.Run(() => datePer.SaveUserInformation());
        }
    }
}
