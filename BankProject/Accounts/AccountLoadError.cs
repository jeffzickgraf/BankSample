using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Accounts
{
	/// <summary>
	/// Represents an issue the loader had with trying to initialize an account.
	/// </summary>
	public class AccountLoadError
	{
		/// <summary>
		/// Initializes an account load error object.
		/// </summary>
		/// <param name="error">The error that occurred.</param>
		/// <param name="accountRowData">The data that was attempted to load.</param>
		public AccountLoadError(string error, string accountRowData)
		{
			Error = error;
			AccountRowData = accountRowData;
		}

		/// <summary>
		/// The error that occurred trying to load the account.
		/// </summary>
		public string Error { get; set; }

		/// <summary>
		/// The account row information that was attempted to load.
		/// </summary>
		public string AccountRowData { get; set; }
	}
}
