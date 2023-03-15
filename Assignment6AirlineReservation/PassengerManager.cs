using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class PassengerManager
    {
        /// <summary>
        /// A list of passengers
        /// </summary>
        private List<Passenger> passengers = new List<Passenger>();

        /// <summary>
        /// A list of passengers
        /// </summary>
        public List<Passenger> Passengers { get { return passengers; } private set { passengers = value; } }

        /// <summary>
        /// The database
        /// </summary>
        private clsDataAccess db;

        /// <summary>
        /// Create a new PassengerManager
        /// </summary>
        /// <param name="db"></param>
        public PassengerManager(clsDataAccess db)
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
        /// Return the list of passengers
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public List<Passenger> GetPassengers(int flightId)
        {
            try
            {
                // Dataset
                DataSet ds;

                string sql = clsSQL.GetPassengers(flightId.ToString());

                // Execute sql
                int rows = 0;
                ds = db.ExecuteSQLStatement(sql, ref rows);

                //Show the data
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int id = 0;
                    string firstName = String.Empty;
                    string lastName = String.Empty;

                    id = (int)row.ItemArray[0];
                    firstName = (string)row.ItemArray[1];
                    lastName = (string)row.ItemArray[2];

                    Passenger passenger = new Passenger(id, firstName, lastName);
                    Passengers.Add(passenger);
                }
                return Passengers;
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return null;
            }
        }

        /// <summary>
        /// Delete a passenger record
        /// Does not automaticly update the list of passengers
        /// </summary>
        /// <param name="PassengerID"></param>
        public void DeletePassenger(int PassengerID)
        {
            try
            {
                // Dataset
                DataSet ds = new DataSet();

                string sql = clsSQL.DeletePassenger(PassengerID.ToString());

                // Execute sql
                int rows = 0;
                db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Add a new passenger
        /// Does not automaticly update the list of passengers
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>New PassengerID</returns>
        public int AddPassenger(string firstName, string lastName)
        {
            try
            {
                //Dataset
                DataSet ds = new DataSet();

                // sql to get max number
                string sql = clsSQL.GetMaxPassengerID();

                // Execute sql
                int rows = 0;
                string maxNumber = db.ExecuteScalarSQL(sql);
                int newNumber = 0;

                Int32.TryParse(maxNumber, out newNumber);
                newNumber++;

                // sql to insert new passenger
                sql = clsSQL.InsertPassenger(newNumber.ToString(), firstName, lastName);
                db.ExecuteNonQuery(sql);

                return newNumber;
            }
            catch(Exception ex)
            {
                ErrorHandling.throwError(MethodInfo.GetCurrentMethod(), ex);
                return -1;
            }
        }
    }
}
