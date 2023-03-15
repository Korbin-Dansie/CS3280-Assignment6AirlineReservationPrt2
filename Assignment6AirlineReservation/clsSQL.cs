using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsSQL
    {
        /// <summary>
        /// SQL statment to get flights 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

        /// <summary>
        /// SQL statment to get passengers
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                              "FROM Passenger, Flight_Passenger_Link FPL " +
                              "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                              "Flight_ID = " + sFlightID;
                return sSQL;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

        /// <summary>
        /// SQL statment to get passenger seats
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public static string GetFlightPassengerLink(string sFlightID)
        {
            try
            {
                string sSQL =   "SELECT Flight_ID, Passenger_ID, Seat_Number " +
                                "FROM Flight_Passenger_Link " +
                                "WHERE Flight_ID = " + sFlightID;
                return sSQL;
            }
            catch(Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

        /// <summary>
        /// SQL statment to update a passengers seat
        /// </summary>
        /// <param name="sNewSeatNumber"></param>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        public static string UpdatePassengerSeat(string sNewSeatNumber, string sFlightID, string sPassengerID)
        {
            try
            {
                string sSQL = "UPDATE Flight_Passenger_Link " +
                              "SET Seat_Number = " + sNewSeatNumber +
                              " WHERE Passenger_ID = " + sPassengerID +
                              " AND Flight_ID = " + sFlightID;
                return sSQL;
            }
            catch(Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a passenger
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        public static string DeletePassenger(string sPassengerID)
        {
            try
            {
                string sSQL = "DELETE FROM Passenger " +
                              " WHERE Passenger_ID = " + sPassengerID;
                return sSQL;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

        /// <summary>
        /// Delete all passenger seats on all flight
        /// As far as I can tell this is how it is supposed to be coded for this assignment
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        public static string DeletePassengerSeat(string sPassengerID)
        {
            try
            {
                string sSQL = "DELETE FROM Flight_Passenger_Link " +
                              " WHERE Passenger_ID = " + sPassengerID;
                return sSQL;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

    }
}
