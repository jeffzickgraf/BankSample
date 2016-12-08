using System.Collections.Generic;
using BankProject.Accounts;

namespace BankProject.Transaction
{
	/// <summary>
	/// Represents the Transaction Processor mechanism.
	/// </summary>
	public interface ITransactionProcessor
	{
		IList<IAccount> Accounts { get; set; }
		IList<ITransaction> Transactions { get; set; }
		void ProcessTransactions();
		void Transact(ITransaction transaction);
	}
}