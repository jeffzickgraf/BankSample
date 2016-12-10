using BankProject.Accounts;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankProject.Output
{
	/// <summary>
	/// Records account data to a file.
	/// </summary>
	public class AccountRecorder
	{
		string _pathToRecordTo;
				
		/// <summary>
		/// Instantiates an instance of the AccountRecorder.
		/// </summary>
		/// <param name="pathToRecordTo">The fully qualified pathname of the file to record to.</param>
		/// <param name="accounts">Accounts to write out to.</param>
		public AccountRecorder(string pathToRecordTo, IList<IAccount> accounts)
		{
			_pathToRecordTo = pathToRecordTo;

			//Instructions note we need to output from least to greatest in numeric order
			Accounts = accounts.OrderBy(o=>o.AccountNumber).ToList();
		}

		public IList<IAccount> Accounts { get; set; }

		/// <summary>
		/// Records the account data to a file.
		/// </summary>
		public void RecordAccounts()
		{ 
			if (File.Exists(_pathToRecordTo))
				File.Delete(_pathToRecordTo);

			using (StreamWriter sw = File.AppendText(_pathToRecordTo))
			{
				foreach (IAccount account in Accounts)
				{
					var accountLine = string.Format("{0}, {1}, {2}, {3}",
										account.AccountNumber,
										account.AccountOwner,
										account.Balance,
										account is BusinessAccount ? "Business" : "Personal"				
					);
					sw.WriteLine(accountLine);
				}				
			}
		}
	}
}
