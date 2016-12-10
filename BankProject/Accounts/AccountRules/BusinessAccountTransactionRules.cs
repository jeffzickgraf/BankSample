using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Accounts.AccountRules
{
    /// <summary>
    /// Transaction rules for Business Accounts.
    /// </summary>
    public class BusinessAccountTransactionRules : ITransactionAccountRules
    {
		/// <summary>
		/// Inidicates if a transaction fee should be charged.
		/// </summary>
		public bool ShouldChargeTransactionFee
        {
            get
            {
                return true;
            }          
        }

		/// <summary>
		/// Indicates if the account should allow overdrafting.
		/// </summary>
		public bool ShouldAllowOverdrafts
        {
            get
            {
                return true;
            }
        }

		/// <summary>
		/// Gets the overdraft fee if applicable.
		/// </summary>
		public decimal OverdraftFee
        {
            get
            {
                return 20m;
            }
        }

		/// <summary>
		/// Gets the overdraft allowance if applicable.
		/// </summary>
		public decimal OverdraftAllowance
        {
            get
            {
                return 1000m;
            }
        }

		/// <summary>
		/// Gets transaction fee if applicable.
		/// </summary>
		public decimal TransactionFee
		{
			get
			{
				return 1.00m;
			}
		}
	}
}
