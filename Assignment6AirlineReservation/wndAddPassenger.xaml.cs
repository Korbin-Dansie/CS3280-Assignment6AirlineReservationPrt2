using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for wndAddPassenger.xaml
    /// </summary>
    public partial class wndAddPassenger : Window
    {
        /// <summary>
        /// The new passenger
        /// </summary>
        Passenger passenger = new Passenger();

        /// <summary>
        /// See if save was clicked
        /// </summary>
        public bool isSaveClicked = false;

        /// <summary>
        /// constructor for the add passenger window
        /// </summary>
        public wndAddPassenger()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// only allows letters to be input
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void txtLetterInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Handels when the user clicks save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool informationCorrect = true;
                string errorMessage = String.Empty;

                //See if both first and last name are filled
                if (String.IsNullOrEmpty(txtFirstName.Text))
                {
                    informationCorrect = false;
                    errorMessage = "Please enter a first name";
                }
                else if (String.IsNullOrEmpty(txtLastName.Text))
                {
                    informationCorrect = false;
                    errorMessage = "Please enter a last name";
                }
                else
                {
                    informationCorrect = true;
                    errorMessage = String.Empty;
                }

                // See if there was an error with the information is correct
                if (!informationCorrect)
                {
                    lbError.Visibility = Visibility.Visible;
                    tbError.Text = errorMessage;
                    return;
                }
                else
                {
                    lbError.Visibility = Visibility.Collapsed;
                }

                isSaveClicked = true;
                this.Close();
            }
            catch(Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Change what happens when the window closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }
    }
}
