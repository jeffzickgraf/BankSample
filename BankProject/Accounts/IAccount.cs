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

		void Deposit(decimal depositAmount);
        TransactionStatus Transfer(IAccount toAccount, decimal amount);
        WithdrawalStatus Withdrawal(decimal withdrawalAmount);

		ITransactionAccountRules AccountRules { get; set; }
    }
}
