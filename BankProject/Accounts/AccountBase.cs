using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProject.Transaction;

namespace BankProject.Accounts
{
    /// <summary>
    /// Base class for accounts. DRY principle - trying to consolidate as much logic that is not divergent as possible.
    /// Also a way to push down required differentiators to our concrete classes where things must vary.
    /// </summary>
    public abstract class AccountBase : IAccount 
    {
        private int _accountNumber;

        /// <summary>
        /// Customer account number.
        /// </summary>                
        public int AccountNumber 
        { 
            get{ return _accountNumber; }
            set 
            {
                //Always better to have the conditional on the most likely satisfied expression
                if (value < 1000000)
                {
                    _accountNumber = value;
                }
                else
                {
                    //Could create a customized exception if desired here dereived from base exception.
                    throw new ArgumentOutOfRangeException("AccountNumber", "Account number can be no larger than 1000000");
                }
            } 
        }
        
        /// <summary>
        /// Account owner name.
        /// </summary>
        public string AccountOwner { get; set; }

        /// <summary>
        /// Gets the balance for the account.  
        /// ---Note: don't want to expose a way to set balance by anyone since its so important- so can't have public setter in base class
        /// </summary>
        public abstract decimal Balance { get; } 

        /// <summary>
        /// Set the balance.
        /// </summary>
        /// <param name="balance"></param>
        protected abstract void SetBalance(decimal balance);

        /// <summary>
        /// Deposits funds into an account
        /// </summary>
        /// <param name="depositAmount">Ammount to deposit.</param>
        public virtual void Deposit(decimal depositAmount)
        {
            SetBalance(Balance + depositAmount);
        }

        /// <summary>
        /// If business rules are met, withdrawal the amount from the account
        /// </summary>
        /// <param name="withdrawalAmount"></param>
        /// <returns>Indication of successful withdrawal</returns>
        public abstract WithdrawalStatus Withdrawal(decimal withdrawalAmount);  //Push this logic to concrete implementation classes

        /// <summary>
        /// Transfers funds from this account to another account.
        /// </summary>
        /// <param name="toAccount">Account to transfer to.</param>
        /// <param name="amount">Ammount to transfer</param>
        /// <returns>Indication if transfer was successful.</returns>
        public TransactionStatus Transfer(IAccount toAccount, decimal amount)
        {
            var status = Withdrawal(amount);
            if (status.WithdrawalSucceeded)
            {
                toAccount.Deposit(amount);
                return new TransactionStatus(){ TransactionSucceeded = true };
            }

            return new TransactionStatus() { TransactionSucceeded = false, Error = status.FailureReason};
        }
    }
}
