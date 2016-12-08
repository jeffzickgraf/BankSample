using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProject.Accounts.AccountRules;

namespace BankProject.Accounts
{
    /// <summary>
    /// Factory to create accounts from initial file load
    /// </summary>
    public class AccountFactory 
    {
        public AccountBase CreateAccount(string accountType, int accountNumber, string accountOwner, decimal initialBalance)
        {
            switch (accountType)
            {
                case "Personal":
                    //Todo: Validate account
                    var personalAccount = new PersonalAccount(accountNumber, accountOwner, initialBalance, new PersonalAccountTransactionRules());
                    return personalAccount;
                case "Business":
                    throw new NotImplementedException();
                    //return checkingAccount;
                default:
                    //Requirement mentions Account Type Capitalization is important so if not typed correctly, throw
                    throw new ArgumentOutOfRangeException("accounttype");
            }
        }
    }
}
