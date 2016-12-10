using System;

namespace BankProject.Accounts.AccountRules
{
	/// <summary>
	/// Transaction rules for Personal Accounts.
	/// </summary>
	public class PersonalAccountTransactionRules : ITransactionAccountRules
    {
		/// <summary>
		/// Inidicates if a transaction fee should be charged.
		/// </summary>
		public bool ShouldChargeTransactionFee
        {
            get { return false; }
        }

		/// <summary>
		/// Indicates if the account should allow overdrafting.
		/// </summary>
		public bool ShouldAllowOverdrafts
        {
            get { return false; }
        }

		/// <summary>
		/// Gets the overdraft fee if applicable.
		/// </summary>
		public decimal OverdraftFee
        {
            get { throw new NotImplementedException(); }
        }

		/// <summary>
		/// Gets the overdraft allowance if applicable.
		/// </summary>
		public decimal OverdraftAllowance
        {
            get { throw new NotImplementedException(); }
        }

		/// <summary>
		/// Gets transaction fee if applicable.
		/// </summary>
		public decimal TransactionFee
		{
			get	{throw new NotImplementedException(); }
		}
	}
}
