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
		/// <summary>
		/// The transactions.
		/// </summary>
		public IList<ITransaction> Transactions { get; set; }

		/// <summary>
		/// The list of accounts to be processed.
		/// </summary>
		public IList<IAccount> Accounts { get; set; }

		/// <summary>
		/// Instantiates the Transaction Processor.
		/// </summary>
		/// <param name="accounts">The list of accounts in the system.</param>
		/// <param name="transactions">The transactions to process.</param>
		public TransactionProcessor(IList<IAccount> accounts, IList<ITransaction> transactions)
		{
			Transactions = transactions;
			Accounts = accounts;
		}

		/// <summary>
		/// Processes all transactions.
		/// </summary>
		/// <returns>An IList of transactions that have been processed.</returns>
		public IList<ITransaction> ProcessTransactions()
		{
			foreach (ITransaction transaction in Transactions.Where(t=>t.IsTransactable))
			{
				Transact(transaction);
			}
			return Transactions;
		}

		private void Transact(ITransaction transaction)
		{
			transaction.TransactionStatus
				= transaction.SourceAccount.Transfer(transaction.DestinationAccount, transaction.TransactionAmount);
			
			//If our rules indicate there should be a transaction fee, charge it
			if (transaction.TransactionStatus.TransactionSucceeded && transaction.SourceAccount.AccountRules.ShouldChargeTransactionFee)
			{
				transaction.SourceAccount.ChargeFee(transaction.SourceAccount.AccountRules.TransactionFee);
			}
		}
	}
}
