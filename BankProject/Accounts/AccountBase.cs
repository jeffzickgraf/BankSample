using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Customer account number.
        /// </summary>                
        public int AccountNumber { get; set; } 

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
        /// ---Note: don't want to expose a way to set balance by anyone since its so important- so didn't want a public setter in base class
        /// </summary>
        public abstract decimal Balance { get; }

		/// <summary>
		/// Force concrete instance to provide rules definition. ---Note: Allows for quick changes to 
		/// rules that could fluctuate depending on market conditions without having to rewrite logic for all concrete classes.
		/// </summary>
		public abstract ITransactionAccountRules AccountRules { get; set; }

		/// <summary>
		/// Set the balance.
		/// </summary>
		/// <param name="balance">The balance to set.</param>
		protected abstract void SetBalance(decimal balance);  //protected here to only expose it in implementation classes and not external objects with reference to it.

        /// <summary>
        /// Deposits funds into an account
        /// </summary>
        /// <param name="depositAmount">Ammount to deposit.</param>
        public virtual void Deposit(decimal depositAmount)
        {
            SetBalance(Balance + depositAmount);
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

				return new TransactionStatus(true);
			}

			return new TransactionStatus(false, status.FailureReason);
		}

		/// <summary>
		/// Deducts a fee from the accounts balance.
		/// </summary>
		/// <param name="fee">The feee to charge</param>		
		public void ChargeFee(decimal fee)
		{
			SetBalance(Balance - fee);
		}

		/// <summary>
		/// If business rules are met, withdrawal the amount from the account.
		/// </summary>
		/// <param name="withdrawalAmount">The amount to attempt to withdrawal.</param>
		/// <returns>Indication of successful withdrawal</returns>
		public virtual WithdrawalStatus Withdrawal(decimal withdrawalAmount)
		{
			decimal expectedBalance = Balance - withdrawalAmount;
			if (expectedBalance >= 0)
			{
				SetBalance(expectedBalance);				
				return new WithdrawalStatus(true, null);
			}

			//Balance would be less than 0 so need to do overdraft logic
			return ProcessOverdraftWidthrawal(withdrawalAmount, expectedBalance);
		}
		
		//Private method to reduce cyclomatic complexity of Withdrawal function.
		private WithdrawalStatus ProcessOverdraftWidthrawal(decimal withdrawalAmount, decimal expectedBalance)
		{
			//Here Use of account rules rather than pushing down to concrete implementations allows us to switch out
			//rules quickly if they change without havint to rewrite core withdrawal code in each concrete class
			if (AccountRules.ShouldAllowOverdrafts)
			{
				if (WontExceedOverdraftAllowance(withdrawalAmount))
				{
					//Keep a running total of overdrafts as the instructions indicated could allow up to $1000 in overdrafts
					AddToAccumulatedOverdraft(withdrawalAmount);

					//Set to expected balance and charge the overdraft fee
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

		private void AddToAccumulatedOverdraft(decimal withdrawalAmount)
		{
			if (Balance > 0)
			{
				//Example: $100 withdrawal - $5 Balance = $95 overage
				Overdrafts.Add(withdrawalAmount - Balance);
			}
			else
			{
				Overdrafts.Add(withdrawalAmount);
			}
			
		}

		private bool WontExceedOverdraftAllowance(decimal withdrawalAmount)
		{
			if (Balance > 0)
			{
				//example: $5 - $100 >= -1 * $1000
				return Balance - withdrawalAmount >= -1 * AccountRules.OverdraftAllowance;
			}
			else
			{
				//example: $-5 - $100 >= -1 * $1000
				return Balance - withdrawalAmount - Overdrafts.Sum() >= -1 * AccountRules.OverdraftAllowance;
			}
			
		}		
    }
}
