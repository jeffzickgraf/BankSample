using BankProject.Accounts.AccountRules;
using BankProject.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Accounts
{
    /// <summary>
    /// Represents a business account.
    /// </summary>
    public class BusinessAccount : AccountBase
    {
		private decimal _balance;

		/// <summary>
		/// Constructor for a Business Account.
		/// </summary>
		/// <param name="accountNumber">The account number.</param>
		/// <param name="accountOwner">The account owner.</param>
		/// <param name="balance">The initial balance.</param>
		/// <param name="rules">Transaction rules for the account type.</param>
		public BusinessAccount(int accountNumber, string accountOwner, decimal balance, ITransactionAccountRules rules)
		{
			AccountOwner = accountOwner;
			AccountNumber = accountNumber;
			_balance = balance;
			Overdrafts = new List<decimal>();
			AccountRules = rules;
		}

		/// <summary>
		/// The account rules to be used for transactions with this account type.
		/// </summary>
		public override ITransactionAccountRules AccountRules { get; set; }

		/// <summary>
		/// The Balance for the account.
		/// </summary>
		public override decimal Balance
		{
			get { return _balance; }
		}

		/// <summary>
		/// Sets the balance for the account.
		/// </summary>
		/// <param name="balance">The amount to set.</param>
		protected override void SetBalance(decimal balance)
		{
			_balance = balance;
		}
	}
}
