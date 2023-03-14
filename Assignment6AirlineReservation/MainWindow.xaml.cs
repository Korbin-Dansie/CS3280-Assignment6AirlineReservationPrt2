using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Data Connection
        /// </summary>
        clsDataAccess clsData;

        /// <summary>
        /// A list of flights
        /// </summary>
        FlightManager flightManager;

        /// <summary>
        /// The add passenger window
        /// </summary>
        wndAddPassenger wndAddPass;

        /// <summary>
        /// constructor for the main window
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                //Create new windows
                wndAddPass = new wndAddPassenger();


                clsData = new clsDataAccess();

                // Get the flights from the data base
                flightManager = new FlightManager(clsData);

                // Load the flights into the combobox
                cbChooseFlight.Items.Clear();
                cbChooseFlight.ItemsSource = flightManager.getFlights();
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Triggers when the flight combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Flight flight = cbChooseFlight.SelectedItem as Flight;

                // If flight is null exit
                if (flight == null)
                {
                    return;
                }

                // Enable input
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;

                // Display the correct canvas
                if (flight.AircraftType == "Boeing 767")
                {
                    toggleFlightCanvases(true);
                }
                else
                {
                    toggleFlightCanvases(false);
                }

                // Fill in the passanger combo box
                PassengerManager passengers = new PassengerManager(clsData);
                passengers.GetPassengers(flight.FlightId);

                // Add the passangers to the combo box
                cbChoosePassenger.ItemsSource = passengers.Passengers;

            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Handels when the user clicks on the add passenger button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();

                //wndAddPass.Show();
                //wndAddPass.Hide();

                relocateWindow(wndAddPass, this);
                wndAddPass.ShowDialog();
                relocateWindow(this, wndAddPass);
                this.Show();
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Toggle the visibility of the two canvases
        /// </summary>
        /// <param name="is767Visible"></param>
        private void toggleFlightCanvases(bool is767Visible)
        {
            try
            {
                if (is767Visible)
                {
                    CanvasA380.Visibility = Visibility.Collapsed;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Relocate the windows to match
        /// </summary>
        /// <param name="toWindow"></param>
        /// <param name="fromWindow"></param>
        private void relocateWindow(Window toWindow, Window fromWindow)
        {
            try
            {
                toWindow.Left = fromWindow.Left;
                toWindow.Top = fromWindow.Top;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }
    }
}
