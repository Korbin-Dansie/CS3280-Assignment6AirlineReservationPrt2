using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6AirlineReservation
{
    internal static class ErrorHandling
    {
        /// <summary>
        /// Handle Errors
        /// </summary>
        /// <param name="method">MethodInfo.GetCurrentMethod()</param>
        /// <param name="ex"></param>
        public static void handleError(MethodBase method, Exception ex)
        {
            try
            {
                string message = getString(method, ex);
                MessageBox.Show(message);
            }
            catch (Exception execption)
            {
                string message = getString(method, ex);
                System.IO.File.AppendAllText(@"C:\Error.txt", "HandleError Exception: " + Environment.NewLine + message);
            }
        }

        /// <summary>
        /// Throw a new error
        /// </summary>
        /// <param name="method">MethodInfo.GetCurrentMethod()</param>
        /// <param name="ex"></param>
        /// <exception cref="Exception"></exception>
        public static void throwError(MethodBase method, Exception ex)
        {
            //throw new Exception(declaringType + "." + name + "->" + Environment.NewLine + message);
            throw new Exception(getString(method, ex));
        }

        /// <summary>
        /// The string of how the error is displayed
        /// </summary>
        /// <param name="method"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string getString(MethodBase method, Exception ex)
        {
            return method.DeclaringType.Name.ToString() + "." + method.Name + "->" + Environment.NewLine + ex.Message;
        }
    }

}
