using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class SeatManager
    {
        /// <summary>
        /// A list of connections
        /// </summary>
        private List<FlightPassengerLink> _information = new List<FlightPassengerLink>();


        /// <summary>
        /// The database
        /// </summary>
        private clsDataAccess db;

        /// <summary>
        /// Returns the list of flights to passengers
        /// </summary>
        public List<FlightPassengerLink> Information
        {
            get { return _information; }
            set { _information = value; }
        }

        /// <summary>
        /// Create a new SeatManager
        /// </summary>
        /// <param name="db"></param>
        public SeatManager(clsDataAccess db)
        {
            this.db = db;
        }

        /// <summary>
        /// Return a list of flight to passengers
        /// plus their seat number
        /// </summary>
        /// <param name="sFlightId"></param>
        /// <returns></returns>
        public List<FlightPassengerLink> GetFlightPassengerLink(int sFlightId)
        {
            try
            {
                // Dataset
                DataSet ds;

                string sql = clsSQL.GetFlightPassengerLink(sFlightId.ToString());

                // Execute sql
                int rows = 0;
                ds = db.ExecuteSQLStatement(sql, ref rows);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int flightNumberId = 0;
                    int passengerNumberId = 0;
                    int seatNumber = 0;

                    flightNumberId = (int)row.ItemArray[0];
                    passengerNumberId = (int)row.ItemArray[1];

                    bool success = Int32.TryParse(row.ItemArray[2].ToString(), out seatNumber);
                    if (!success)
                    {
                        ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), new Exception("Could not convert Seat_Number to an int"));
                    }

                    FlightPassengerLink fpl = new FlightPassengerLink(flightNumberId, passengerNumberId, seatNumber);
                    Information.Add(fpl);
                }
                return Information;

            }
            catch(Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

    }
}
