using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class Passenger
    {
        /// <summary>
        /// Passenger Id
        /// </summary>
        private int _passengerId;

        /// <summary>
        /// Passenger first name
        /// </summary>
        private string _passengerFirstName;

        /// <summary>
        /// Passenger last name
        /// </summary>
        private string _passengerLastName;

        /// <summary>
        /// Passenger Id
        /// </summary>
        public int PassengerId { get { return _passengerId; } set { _passengerId = value; } }

        /// <summary>
        /// Passenger fist name
        /// </summary>
        public string PassengerFirstName { get { return _passengerFirstName; } set { _passengerFirstName = value; } }

        /// <summary>
        /// Passenger last name
        /// </summary>
        public string PassengerLastName { get { return _passengerLastName; } set { _passengerLastName = value; } }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Passenger()
        {
            PassengerId = 0;
            PassengerFirstName = string.Empty;
            PassengerLastName = string.Empty;
        }

        /// <summary>
        /// Create a new passenger
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="passengerFirstName"></param>
        /// <param name="passengerLastName"></param>
        public Passenger(int passengerId, string passengerFirstName, string passengerLastName)
        {
            PassengerId = passengerId;
            PassengerFirstName = passengerFirstName;
            PassengerLastName = passengerLastName;
        }

        /// <summary>
        /// Returns first and last name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return PassengerFirstName + " " + PassengerLastName;
        }
    }

}
