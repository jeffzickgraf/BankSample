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
       bool ShouldChargeTransactionFee { get; }
	   decimal TransactionFee { get; }
       bool ShouldAllowOverdrafts { get; }
       decimal OverdraftFee { get; }
       decimal OverdraftAllowance { get; }
    }
}
