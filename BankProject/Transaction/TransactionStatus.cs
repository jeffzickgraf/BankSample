using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Transaction
{
    /// <summary>
    /// Indication of success or failure of a transaction and error reason if failure.
    /// </summary>
    public class TransactionStatus
    {
		/// <summary>
		/// Instantiates a transaction status.
		/// </summary>
		/// <param name="succeeded">Indication of whether the transaction succeeded or failed.</param>
		/// <param name="error">Leave empty if succeeded, if failed add error reason.</param>
		public TransactionStatus(bool succeeded, string error = "")
		{
			TransactionSucceeded = succeeded;
			Error = error;
		}

		/// <summary>
		/// Indicates success or failure of the transaction.
		/// </summary>
        public bool TransactionSucceeded { get; set; }

		/// <summary>
		/// If failure indicates why the transaction was not successful.
		/// </summary>
        public string Error { get; set; }

    }
}
