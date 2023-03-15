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
using System.Windows.Media.Media3D;
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
        private clsDataAccess clsData;

        /// <summary>
        /// The currently selected flight
        /// </summary>
        private Flight currentSelectedFlight;

        /// <summary>
        /// A list of flights
        /// </summary>
        private FlightManager flightManager;

        /// <summary>
        /// A list of flightId, passengerId, and seatNumbers
        /// </summary>
        private SeatManager seatManager;

        /// <summary>
        /// The add passenger window
        /// </summary>
        private wndAddPassenger wndAddPass;

        /// <summary>
        /// Current seat selection mode
        /// </summary>
        private SeatSelectionMode currentSeatSelectionMode = SeatSelectionMode.Regular;

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

                currentSelectedFlight = flight;

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
                reloadPassengers();
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Refill the passenger combo box
        /// and update seat labels
        /// </summary>
        private void reloadPassengers()
        {
            PassengerManager passengers = new PassengerManager(clsData);
            passengers.GetPassengers(currentSelectedFlight.FlightId);

            // Fill in the seat manager
            seatManager = new SeatManager(clsData);
            seatManager.GetFlightPassengerLink(currentSelectedFlight.FlightId);

            // Add the passangers to the combo box
            cbChoosePassenger.ItemsSource = passengers.Passengers;
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

        /// <summary>
        /// Color all the empty seats blue
        /// And all the taken seats red
        /// </summary>
        private void FillPassengerSeats()
        {
            // Reset all eats in the selecte flight to blue
            // Loop through each passenger in the list
            // Then loop through each seat in the selected flight "c767_Seats.Children"
            // Then compare the passengers seat to the labels content and if they match, then change the background to red because the seat is taken

        }

        /// <summary>
        /// Enables / Disables all the UI components
        /// </summary>
        private void toggleInput(bool enable)
        {
            cbChooseFlight.IsEnabled = enable;
            cbChoosePassenger.IsEnabled = enable;

            cmdChangeSeat.IsEnabled = enable;
            cmdDeletePassenger.IsEnabled = enable;
            cmdAddPassenger.IsEnabled = enable;
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
                relocateWindow(wndAddPass, this);
                wndAddPass.ShowDialog();

                // Check the add passenger window to see if the user clicked sabve and if they did, then
                // disable everything except the seats, so they are frced to click a seat

                // Set the variable that tell that the user is in  Add passenger mode

                relocateWindow(this, wndAddPass);
                this.Show();
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            // Passenger is selected 
            // Lock down window and set to change seat mode, force user to select a seat
        }

        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            // Passenger is selected
            // Delete the currently selected passenger (Done in another class)
            // Relaod the passenger into the combo box
            // Reload the taken seats
        }

        /// <summary>
        /// This method will get called when the user click on any seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            // What mode is the progarm in? Add passenger, Change Seat, or regular
            switch (currentSeatSelectionMode)
            {
                // Add passenger Mode
                // Insert a new passenger into the database, then insert a record into the link table (Done in another class)
                case SeatSelectionMode.Add:
                    break;
                // Change seat mode
                // Only change the seat if the seat is empty
                // If it's empty then update the link table to update the users new seat (Done in another class)
                case SeatSelectionMode.Change:
                    break;
                // Otherwise in regular seat selection mode
                // If a seat is taken, then loop through that passenger in the combo box
                // and keep looping until the seat that was clicked, its number matcher a passengers seat number
                // then select that combo box index or selected item and put the passengers seat in the label (lblPassengersSeatNumber)
                default:

                    break;

            }
        }

        /// <summary>
        /// Different states the seats could be in
        /// </summary>
        private enum SeatSelectionMode
        {
            Regular,
            Add,
            Change
        }
    }
}
