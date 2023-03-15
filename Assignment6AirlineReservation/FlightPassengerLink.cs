using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class FlightPassengerLink
    {
        /// <summary>
        /// The flight Id
        /// </summary>
        int _flight_Id;

        /// <summary>
        /// The passenger Id
        /// </summary>
        int _passenger_Id;

        /// <summary>
        /// The seat number
        /// </summary>
        string _seatNumber;


        /// <summary>
        /// The flight Id
        /// </summary>
        public int Flight_Id { 
            get { return _flight_Id; } 
            set { _flight_Id = value; } 
        }

        /// <summary>
        /// The passenger Id
        /// </summary>
        public int Passenger_Id {
            get { return _passenger_Id; }
            set { _passenger_Id = value; }
        }

        /// <summary>
        /// The seat number
        /// </summary>
        public string SeatNumber { 
            get { return _seatNumber; }
            set { _seatNumber = value; }
        }

        /// <summary>
        /// Create a new FlightSeatLink
        /// </summary>
        /// <param name="flight_Id"></param>
        /// <param name="passenger_id"></param>
        /// <param name="seatNumber"></param>
        public FlightPassengerLink(int flight_Id, int passenger_id, string seatNumber)
        {
            Flight_Id = flight_Id;
            Passenger_Id = passenger_id;
            SeatNumber = seatNumber;
        }
    }
}
