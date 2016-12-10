using BankProject.Accounts;
using System.Collections.Generic;
using System.IO;

namespace BankProject.Output
{
	/// <summary>
	/// Records account data to a file.
	/// </summary>
	public class AccountRecorder
	{
		string _pathToRecordTo;
		IList<IAccount> _accounts;
		/// <summary>
		/// Instantiates an instance of the AccountRecorder.
		/// </summary>
		/// <param name="pathToRecordTo">The fully qualified pathname of the file to record to.</param>
		/// <param name="accounts">Accounts to write out to.</param>
		public AccountRecorder(string pathToRecordTo, IList<IAccount> accounts)
		{
			_pathToRecordTo = pathToRecordTo;
			_accounts = accounts;
		}
		
		/// <summary>
		/// Records the account data to a file.
		/// </summary>
		public void RecordAccounts()
		{ 
			if (File.Exists(_pathToRecordTo))
				File.Delete(_pathToRecordTo);

			using (StreamWriter sw = File.AppendText(_pathToRecordTo))
			{
				foreach (IAccount account in _accounts)
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
