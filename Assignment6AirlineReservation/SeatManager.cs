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
            try
            {
                this.db = db;
            }
            catch(Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
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
                    string seatNumber = String.Empty;

                    flightNumberId = (int)row.ItemArray[0];
                    passengerNumberId = (int)row.ItemArray[1];

                    seatNumber = row.ItemArray[2].ToString();

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

        /// <summary>
        /// Move a passenger to another seat
        /// Does not update it's list automaticly with the new information
        /// </summary>
        /// <param name="NewSeatNumber"></param>
        /// <param name="FlightID"></param>
        /// <param name="PassengerID"></param>
        public void updateSeat(string NewSeatNumber, int FlightID, int PassengerID)
        {
            try
            {
                // Dataset
                DataSet ds = new DataSet();

                string sql = clsSQL.UpdatePassengerSeat(NewSeatNumber, FlightID.ToString(), PassengerID.ToString());

                // Execute sql
                int rows = 0;
                db.ExecuteNonQuery(sql);
            }
            catch(Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Delete a passengers seat on all flight
        /// As far as I can tell this is how its supposed to be programed
        /// </summary>
        /// <param name="PassengerID"></param>
        public void DeleteSeat(int PassengerID)
        {
            try
            {
                // Dataset
                DataSet ds = new DataSet();

                string sql = clsSQL.DeletePassengerSeat(PassengerID.ToString());

                // Execute sql
                int rows = 0;
                db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }


    }
}
