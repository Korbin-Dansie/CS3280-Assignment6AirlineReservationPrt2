using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class Flight
    {
        /// <summary>
        /// Flight Id
        /// </summary>
        private int _flightId;

        /// <summary>
        /// Flight number
        /// </summary>
        private string _flightNumber;

        /// <summary>
        /// Aircraft Type
        /// </summary>
        private string _aircraftType;

        /// <summary>
        /// Flight Id
        /// </summary>
        public int FlightId { get { return _flightId; } set { _flightId = value; } }

        /// <summary>
        /// Flight number
        /// </summary>
        public string FlightNumber { get { return _flightNumber; } set { _flightNumber = value; } }

        /// <summary>
        /// Aircraft Type
        /// </summary>
        public string AircraftType { get { return _aircraftType; } set { _aircraftType = value; } }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Flight()
        {
            try
            {
                FlightId = 0;
                FlightNumber = String.Empty;
                AircraftType = String.Empty;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Constructor to create a new flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="flightNumber"></param>
        /// <param name="aircraftType"></param>
        public Flight(int flightId, string flightNumber, string aircraftType)
        {
            try
            {
                FlightId = flightId;
                FlightNumber = flightNumber;
                AircraftType = aircraftType;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Returns AircraftType - FlightNumber
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return AircraftType + " - " + FlightNumber;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }
    }

}
