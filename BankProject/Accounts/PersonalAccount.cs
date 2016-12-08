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
    /// Represents a personal account.
    /// </summary>
    public class PersonalAccount : AccountBase
    {
        private decimal _balance;
        private ITransactionAccountRules _rules;

        public PersonalAccount(int accountNumber, string accountOwner, decimal balance, ITransactionAccountRules rules)
        {
            AccountOwner = accountOwner;
            AccountNumber = accountNumber;
            _balance = balance;
        }

        public override WithdrawalStatus Withdrawal(decimal withdrawalAmount)
        {
            //todo: use our rules to see if we can withdrawal
            throw new NotImplementedException();
        }


        public override decimal Balance
        {
            get { return _balance; }
        }

        protected override void SetBalance(decimal balance)
        {
            _balance = balance;
        }
    }
}
