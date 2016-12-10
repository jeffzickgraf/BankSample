using BankProject.Accounts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Transaction
{
	/// <summary>
	/// Loads transactions into the system.
	/// </summary>
	public class TransactionLoader
	{
		string fileLocation;
		IList<IAccount> accounts;
		public TransactionLoader(string transactionsFilePath, IList<IAccount> accountList)
		{
			fileLocation = transactionsFilePath;
			accounts = accountList;
		}

		/// <summary>
		/// Processess the transactions of the flat file.
		/// </summary>
		/// <returns>List of attempted transactions.</returns>
		public IList<ITransaction> TransformTransactions()
		{
			IList<ITransaction> transactions = new List<ITransaction>();
			Transaction transaction;

			var fileLines = File.ReadAllLines(fileLocation).Select(csv => csv.Split(','));

			foreach (var item in fileLines)
			{
				string errorMessage;
				decimal amount;

				var transactString = String.Join(",", item);
				var sourceAccount = accounts.FirstOrDefault(sa => sa.AccountNumber.ToString() == item[0].Trim());
				var destinationAccount = accounts.FirstOrDefault(da => da.AccountNumber.ToString() == item[1].Trim());

				
				bool amountValid = decimal.TryParse(item[2].Trim(), out amount);

				var isValidTransaction = IsValidTransaction(item, sourceAccount, destinationAccount, amountValid, out errorMessage);

				if (isValidTransaction)
				{
					transaction = new Transaction(sourceAccount, destinationAccount, amount, transactString);					
				}
				else
				{
					transaction = new Transaction(transactString, new TransactionStatus(false, errorMessage));
				}

				transactions.Add(transaction);
			}
			return transactions;
		}

		private static bool IsValidTransaction(string[] item, IAccount sourceAccount, IAccount destinationAccount, 
			bool amountValid, out string errorMessage)
		{
			errorMessage = string.Empty;

			if (sourceAccount == null)
				errorMessage += string.Format("Source Account {0} not found. ", item[0]);

			if (destinationAccount == null)
				errorMessage += string.Format("Destination Account {0} not found. ", item[1]);

			if (!amountValid)
				errorMessage += string.Format("Amount {0} not in a valid format.", item[2]);

			return String.IsNullOrWhiteSpace(errorMessage);
		}
	}
}
