using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankProject.Accounts
{
	/// <summary>
	/// Loads accounts and initializes their balance.
	/// </summary>
	public class AccountLoader
	{
		string fileLocation;		
		IAccountFactory accountFactory;
		public AccountLoader(string accountsFilePath, IAccountFactory accountCreationFactory)
		{
			fileLocation = accountsFilePath;			
			accountFactory = accountCreationFactory;
		}

		/// <summary>
		/// Loads accounts and initializes there balance into the system.
		/// </summary>
		/// <param name="accountLoadErrors">Outs a list of account errors while trying tor process.</param>
		/// <returns>A list of accounts that were able to be loaded and initialized.</returns>
		public IList<IAccount> InitializeAccounts(out IList<AccountLoadError> accountLoadErrors)
		{
			IList<IAccount> accounts = new List<IAccount>();
			var fileLines = File.ReadAllLines(fileLocation).Select(csv => csv.Split(','));
			accountLoadErrors = new List<AccountLoadError>();
			
			foreach (var item in fileLines)
			{
				string errorMessage = string.Empty; //could use string builder here for more efficiency
				string accountOwner;
				int accountNumber;
				decimal initialBalance;
				string accountType;

				var accountInitRow = String.Join(",", item);

				bool accountNumberValid = int.TryParse(item[0].Trim(), out accountNumber);
				accountOwner = item[1].Trim();
				bool balanceValid = decimal.TryParse(item[2].Trim(), out initialBalance);
				accountType = item[3].Trim();

				if (!accountNumberValid)
					errorMessage += string.Format("Account number {0} not valid. ", item[0]);

				if(!balanceValid)
					errorMessage += string.Format("Account balance {0} not valid. ", item[2]);

				if (!String.IsNullOrWhiteSpace(errorMessage))
					accountLoadErrors.Add(new AccountLoadError(errorMessage, accountInitRow));

				try
				{
					IAccount account = accountFactory.CreateAccount(accountType, accountNumber, accountOwner, initialBalance);
					accounts.Add(account);
				}
				catch (Exception ex)
				{
					//Invalid account types are thrown from the factory so will capture those here as well.
					accountLoadErrors.Add(new AccountLoadError(ex.Message, accountInitRow));					
				}
			}

			return accounts;			
		}		
	}
}
