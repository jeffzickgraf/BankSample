using System.Collections.Generic;
using BankProject.Accounts;

namespace BankProject.Transaction
{
	/// <summary>
	/// Represents the Transaction Processor mechanism.
	/// </summary>
	public interface ITransactionProcessor
	{
		/// <summary>
		/// The list of all accounts.
		/// </summary>
		IList<IAccount> Accounts { get; set; }

		/// <summary>
		/// The Transactions to be processed.
		/// </summary>
		IList<ITransaction> Transactions { get; set; }

		/// <summary>
		/// Processes all transactions.
		/// </summary>
		/// <returns>An IList of transactions that have been processed.</returns>
		IList<ITransaction> ProcessTransactions();
	}
}