using BankProject.Accounts;

namespace BankProject.Transaction
{
	/// <summary>
	/// Object of record for a transaction for the system.
	/// </summary>
	public class Transaction : ITransaction
	{
		/// <summary>
		/// Instantiates a Transaction.
		/// </summary>
		/// <param name="sourceAccount">The source account for the transaction.</param>
		/// <param name="destinationAccount">The destination account for the transaction.</param>
		/// <param name="transactionAmount">The transaction ammount.</param>
		/// <param name="transactionString">The string data from the input of transactions.</param>
		public Transaction(IAccount sourceAccount, IAccount destinationAccount, decimal transactionAmount, string transactionString)
		{
			SourceAccount = sourceAccount;
			DestinationAccount = destinationAccount;
			TransactionAmount = transactionAmount;
			IsTransactable = true;
			TransactionString = transactionString;
		}

		/// <summary>
		/// Alternat constructor in the event we have a malformed transaction.
		/// </summary>
		/// <param name="transactionString">The input data for the transaction.</param>
		/// <param name="transactionStatus">The status of the transaction</param>
		/// <param name="isTransactable">Indication if the status of the transaction should be attempted.</param>
		public Transaction(string transactionString, TransactionStatus transactionStatus, bool isTransactable = false)
		{
			TransactionString = transactionString;
			IsTransactable = isTransactable;
			TransactionStatus = transactionStatus;
		}
		
		/// <summary>
		/// The destination account for the transaction ammount.
		/// </summary>
		public IAccount DestinationAccount { get; set; }

		/// <summary>
		/// The Source account for the transaction.
		/// </summary>
		public IAccount SourceAccount { get; set; }

		/// <summary>
		/// The transaction amount.
		/// </summary>
		public decimal TransactionAmount { get; set; }

		/// <summary>
		/// The transaction status and if unsucessful, contains the error message.
		/// </summary>
		public TransactionStatus TransactionStatus { get; set; }

		/// <summary>
		/// Indicates the transaction data is usable and not malformed or errant datawise.
		/// </summary>		
		public bool IsTransactable { get; set; }


		/// <summary>
		/// The string representation of the transaction from the import file.
		/// Useful in the event of inability to create the transaction object due to errant data so we still have a record of the inbound transaction.
		/// </summary>
		public string TransactionString { get; set; }

	}
}
