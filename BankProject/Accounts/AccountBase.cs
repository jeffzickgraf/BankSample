using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProject.Transaction;
using BankProject.Accounts.AccountRules;

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
		/// Running list of overdrafts 
		/// -- note: in instructions it says they are allowed up to $1,000 in overdrafts so I assume these can accumulate
		/// -- if there is more than one overdraft transaction
		/// </summary>
		public virtual IList<decimal> Overdrafts {get; set;}
        
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
		/// Force concrete instance to provide rules definition.
		/// </summary>
		public abstract ITransactionAccountRules AccountRules { get; set; }

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
        /// If business rules are met, withdrawal the amount from the account.
        /// </summary>
        /// <param name="withdrawalAmount">The amount to attempt to withdrawal.</param>
        /// <returns>Indication of successful withdrawal</returns>
        public virtual WithdrawalStatus Withdrawal(decimal withdrawalAmount)
		{
			decimal expectedBalance = Balance - withdrawalAmount;
			if (expectedBalance < 0)
			{
				SetBalance(expectedBalance);				
				return new WithdrawalStatus(true, null);
			}

			return ProcessOverdraftWidthrawal(withdrawalAmount, expectedBalance);
		}


		//Private method to reduce cyclomatic complexity of Withdrawal function.
		private WithdrawalStatus ProcessOverdraftWidthrawal(decimal withdrawalAmount, decimal expectedBalance)
		{
			if (AccountRules.ShouldAllowOverdrafts)
			{
				if (WontExceedOverdraftAllowance(expectedBalance))
				{
					Overdrafts.Add(Balance - expectedBalance);
					SetBalance(expectedBalance - AccountRules.OverdraftFee);
					return new WithdrawalStatus(true, null);
				}
				else
				{
					return new WithdrawalStatus(false,
						string.Format("Balance of ${0}. Withdrawal limit of ${1} would exceed overdraft limit of {2}",
							Balance, withdrawalAmount, AccountRules.OverdraftAllowance));
				}
			}
			else
			{
				return new WithdrawalStatus(false,
					string.Format("Balance of ${0}. Not enough funds to withdrawal ${1}", Balance, withdrawalAmount));
			}
		}

		private bool WontExceedOverdraftAllowance(decimal expectedBalance)
		{
			return Balance - expectedBalance - Overdrafts.Sum() <= AccountRules.OverdraftAllowance;
		}

				
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
