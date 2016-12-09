using BankProject.Accounts;

namespace BankProject.Transaction
{
	/// <summary>
	/// Represents a transaction in the system.
	/// </summary>
	public interface ITransaction
	{
		/// <summary>
		/// The destination account for the transaction ammount.
		/// </summary>
		IAccount DestinationAccount { get; set; }
		
		/// <summary>
		/// The Source account for the transaction.
		/// </summary>
		IAccount SourceAccount { get; set; }

		/// <summary>
		/// The transaction amount.
		/// </summary>
		decimal TransactionAmount { get; set; }

		/// <summary>
		/// The transaction status and if unsucessful, contains the error message.
		/// </summary>
		TransactionStatus TransactionStatus { get; set; }

		/// <summary>
		/// Indicates the transaction data is usable and not malformed or errant datawise.
		/// </summary>		
		bool IsTransactable { get; set; }

		/// <summary>
		/// The string representation of the transaction from the import file.
		/// </summary>
		string TransactionString { get; set; }
	}
}