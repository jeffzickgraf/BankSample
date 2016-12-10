using BankProject.Accounts;
using BankProject.Transaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Output
{
	/// <summary>
	/// Records errors of the application, account loading and transactions to a file.
	/// </summary>
	public class ErrorRecorder
	{
		string _pathToRecordTo;
		IList<ITransaction> _failedTransactions;
		IList<AccountLoadError> _accountLoadErrors;
		IList<string> _applicationErrors;

		/// <summary>
		/// Instantiates and ErrorRecorder.
		/// </summary>
		/// <param name="pathToRecordTo">Full pathname to record an errors file to.</param>
		/// <param name="failedTransactions">The system failed transactions.</param>
		/// <param name="accountLoadErrors">The accounts that failed to load and initialize.</param>
		/// <param name="applicationErrors">The applications general errors.</param>
		public ErrorRecorder(string pathToRecordTo, IList<ITransaction> failedTransactions,
			IList<AccountLoadError> accountLoadErrors, 
			IList<string> applicationErrors)
		{
			_pathToRecordTo = pathToRecordTo;
			_failedTransactions = failedTransactions;
			_accountLoadErrors = accountLoadErrors;
			_applicationErrors = applicationErrors;
		}

		/// <summary>
		/// Record errors to a file for viewing.
		/// </summary>
		public void RecordErrors()
		{
			if (File.Exists(_pathToRecordTo))
				File.Delete(_pathToRecordTo);

			using (StreamWriter sw = File.AppendText(_pathToRecordTo))
			{
				//Output a header row so easier to understand results when opening file
				sw.WriteLine(string.Format("{0},{1},{2}",
										"Error Type",
										"Error",
										"Errant Data"
										));

				WriteApplicationErrors(sw);

				WriteAccountLoadErrors(sw);

				WriteFailedTransactionErrors(sw);
			}
		}

		private void WriteApplicationErrors(StreamWriter sw)
		{
			foreach (string appError in _applicationErrors)
			{
				var appErrorLine = string.Format("{0}, {1}, {2}",
									"ApplicationError",
									appError.RemoveLineEndings().ReplaceCommasForCSV(),
									string.Empty
				);
				sw.WriteLine(appErrorLine);
			}
		}

		private void WriteFailedTransactionErrors(StreamWriter sw)
		{
			foreach (ITransaction failedTransaction in _failedTransactions)
			{
				var transactionFailLine = string.Format("{0}, {1}, {2}",
									"TransactionError",
									failedTransaction.TransactionStatus.Error.RemoveLineEndings(),
									//escape commas in our account loaded data so we don't blow up csv file
									failedTransaction.TransactionString.ReplaceCommasForCSV()
				);
				sw.WriteLine(transactionFailLine);
			}
		}

		private void WriteAccountLoadErrors(StreamWriter sw)
		{
			foreach (AccountLoadError accountLoadError in _accountLoadErrors)
			{
				var accountLine = string.Format("{0}, {1}, {2}",
									"AccountLoadError",
									accountLoadError.Error.RemoveLineEndings(),
									//escape commas in our account loaded data so we don't blow up csv file
									 accountLoadError.AccountRowData.ReplaceCommasForCSV()
				);
				sw.WriteLine(accountLine);
			}
		}
	}
}
