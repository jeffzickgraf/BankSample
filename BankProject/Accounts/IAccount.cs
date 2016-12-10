using BankProject.Accounts.AccountRules;
using BankProject.Transaction;
using System;
namespace BankProject.Accounts
{
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

		void Deposit(decimal depositAmount);
        TransactionStatus Transfer(IAccount toAccount, decimal amount);
        WithdrawalStatus Withdrawal(decimal withdrawalAmount);

		ITransactionAccountRules AccountRules { get; set; }
    }
}
