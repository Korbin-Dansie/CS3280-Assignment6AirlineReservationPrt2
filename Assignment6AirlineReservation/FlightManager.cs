using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class FlightManager
    {
        /// <summary>
        /// A list of flights
        /// </summary>
        private List<Flight> flights = new List<Flight>();

        /// <summary>
        /// A list of flights
        /// </summary>
        public List<Flight> Flights { get { return flights; } private set { flights = value; } }

        /// <summary>
        /// The database
        /// </summary>
        private clsDataAccess db;

        /// <summary>
        /// Create a new FlightManager
        /// </summary>
        /// <param name="db">The database</param>
        public FlightManager(clsDataAccess db)
        {
            this.db = db;
        }

        /// <summary>
        /// Return the list of flights
        /// </summary>
        /// <returns></returns>
        public List<Flight> getFlights()
        {
            try
            {
                // Dataset
                DataSet ds;

                string sql = clsSQL.GetFlights();

                // Execute sql
                int rows = 0;
                ds = db.ExecuteSQLStatement(sql, ref rows);

                //Show the data
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int id = 0;
                    string flightNumber = String.Empty;
                    string aircraftType = String.Empty;

                    id = (int)row.ItemArray[0];
                    flightNumber = (string)row.ItemArray[1];
                    aircraftType = (string)row.ItemArray[2];

                    Flight flight = new Flight(id, flightNumber, aircraftType);
                    Flights.Add(flight);
                }

                // Loop through DataSet, for each Row, Create a new Flight, fill it up, add it to the list
                return Flights;
            }
            catch (Exception ex)
            {
                ErrorHandling.handleError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }
    }

}
