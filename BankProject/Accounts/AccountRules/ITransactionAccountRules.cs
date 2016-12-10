using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Accounts.AccountRules
{
    /// <summary>
    /// Represents the possible account rules that should be applied during a transaction.
    /// Can use this as strategy pattern to quickly substitute out a different implementation of rules when they change
    /// or new account types are added.
    /// </summary>
    public interface ITransactionAccountRules
    {
		/// <summary>
		/// Inidicates if a transaction fee should be charged.
		/// </summary>
       bool ShouldChargeTransactionFee { get; }
		
		/// <summary>
		/// Gets transaction fee if applicable.
		/// </summary>
	   decimal TransactionFee { get; }

		/// <summary>
		/// Indicates if the account should allow overdrafting.
		/// </summary>
       bool ShouldAllowOverdrafts { get; }

		/// <summary>
		/// Gets the overdraft fee if applicable.
		/// </summary>
       decimal OverdraftFee { get; }

		/// <summary>
		/// Gets the overdraft allowance if applicable.
		/// </summary>
       decimal OverdraftAllowance { get; }
    }
}
