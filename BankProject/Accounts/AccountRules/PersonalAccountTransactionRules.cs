using System;

namespace BankProject.Accounts.AccountRules
{
	public class PersonalAccountTransactionRules : ITransactionAccountRules
    {
        public bool ShouldChargeTransactionFee
        {
            get { return false; }
        }

        public bool ShouldAllowOverdrafts
        {
            get { return false; }
        }

        public decimal OverdraftFee
        {
            get { throw new NotImplementedException(); }
        }

        public decimal OverdraftAllowance
        {
            get { throw new NotImplementedException(); }
        }

		public decimal TransactionFee
		{
			get	{throw new NotImplementedException(); }
		}
	}
}
