using BankProject.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace BankProject.Transaction
{
	/// <summary>
	/// Responsible for processing the transactions.
	/// </summary>
	public class TransactionProcessor : ITransactionProcessor
	{
		public IList<ITransaction> Transactions { get; set; }
		public IList<IAccount> Accounts { get; set; }

		public TransactionProcessor(IList<IAccount> accounts, IList<ITransaction> transactions)
		{
			Transactions = transactions;
			Accounts = accounts;
		}

		public void ProcessTransactions()
		{
			foreach (ITransaction transaction in Transactions.Where(t=>t.IsTransactable))
			{
				Transact(transaction);
			}
		}
				
		public void Transact(ITransaction transaction)
		{
			transaction.TransactionStatus
				= transaction.SourceAccount.Transfer(transaction.DestinationAccount, transaction.TransactionAmount);
		}
	}
}
