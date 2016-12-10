using BankProject.Accounts.AccountRules;
using BankProject.Transaction;
using System;
namespace BankProject.Accounts
{
	/// <summary>
	/// Represents an account in the system.
	/// </summary>
    public interface IAccount
    {
        int AccountNumber { get; set; }
        string AccountOwner { get; set; }
        decimal Balance { get; }

		/// <summary>
		/// Deducts a fee from the accounts balance.
		/// </summary>
		/// <param name="fee">The feee to charge</param>		
		void ChargeFee(decimal fee);

		/// <summary>
		/// Deposit to the account.
		/// </summary>
		/// <param name="depositAmount">Amount to deposit.</param>
		void Deposit(decimal depositAmount);

		/// <summary>
		/// Attempts to transfer funds from this account to a destination account.
		/// </summary>
		/// <param name="toAccount">The destination account.</param>
		/// <param name="amount">Amount to transfer.</param>
		/// <returns>Indication of success or failure as <see cref="Transaction.TransactionStatus"/>.</returns>
        TransactionStatus Transfer(IAccount toAccount, decimal amount);

		/// <summary>
		/// Attempts to withdrawal funds from the account.
		/// </summary>
		/// <param name="withdrawalAmount">Amount to withdrawal.</param>
		/// <returns>Indication of the success or failure of the withdrawal as <see cref="Transaction.WithdrawalStatus"/>.</returns>
        WithdrawalStatus Withdrawal(decimal withdrawalAmount);

		/// <summary>
		/// Gets or sets the account rules to be used with the account.
		/// </summary>
		ITransactionAccountRules AccountRules { get; set; }
    }
}
