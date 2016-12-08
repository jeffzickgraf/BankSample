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
        public bool ShouldChargeTransactionFee
        {
            get
            {
                return true;
            }          
        }

        public bool ShouldAllowOverdrafts
        {
            get
            {
                return true;
            }
        }

        public decimal OverdraftFee
        {
            get
            {
                return 20m;
            }
        }

        public decimal OverdraftAllowance
        {
            get
            {
                return 1000m;
            }
        }

		public decimal TransactionFee
		{
			get
			{
				return 1.00m;
			}
		}
	}
}
