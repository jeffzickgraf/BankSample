using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProject.Accounts.AccountRules;

namespace BankProject.Accounts
{
    /// <summary>
    /// Factory to create accounts from initial file load.
    /// </summary>
    public class AccountFactory 
    {
		/// <summary>
		/// Initializes an account based on the account type.
		/// </summary>
		/// <param name="accountType">String representation of the account type.</param>
		/// <param name="accountNumber">The account number.</param>
		/// <param name="accountOwner">The account owner.</param>
		/// <param name="initialBalance">The initial balance for the account.</param>
		/// <returns></returns>
        public AccountBase CreateAccount(string accountType, int accountNumber, string accountOwner, decimal initialBalance)
        {
            switch (accountType)
            {
                case "Personal":
                    //Todo: Validate account number and duplicates
                    var personalAccount = new PersonalAccount(accountNumber, accountOwner, initialBalance, new PersonalAccountTransactionRules());
                    return personalAccount;
                case "Business":
					//Todo: Validate account number and duplicates
					var businessAccount = new BusinessAccount(accountNumber, accountOwner, initialBalance, new BusinessAccountTransactionRules());
					return businessAccount;
				default:
                    //Requirement mentions Account Type Capitalization is important so if not typed correctly, throw
                    throw new ArgumentOutOfRangeException("accounttype", 
						string.Format("Unknown or malformed account type of {0}.", accountType));
            }
        }
    }
}
