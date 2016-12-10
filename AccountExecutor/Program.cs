using BankProject.Accounts;
using BankProject.Output;
using BankProject.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountExecutor
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args == null || args.Count() != 4)
			{
				Console.WriteLine("Please specify the following 4 arguments:");
				Console.WriteLine("	1) Path to the transaction file to execute.");
				Console.WriteLine("	2) Path to the accounts file to initially load.");
				Console.WriteLine("	3) Output path for accounts post execution.");
				Console.WriteLine("	4) Output path for errors encountered.");
				Console.WriteLine("	");
				Console.WriteLine(@"	Example: AccountExecutor.exe c:\TransactionPath\t.dat c:\AccountsPath\a.dat c:\OutAccountsPath\a.dat c:\OutErrorsPath\e.csv");

				Console.ReadKey();
			}

			string transactionsPath = args[0];
			string accountsPath = args[1];
			string postExecutionAccountsPath = args[2];
			string errorPath = args[3];

			IList<string> applicationErrorMessages = new List<string>();
			IList<ITransaction> failedTransactions = new List<ITransaction>();
			IList<AccountLoadError> accountLoadErrors = new List<AccountLoadError>();

			try
			{
				//Load our accounts up
				Console.WriteLine("Loading accounts file {0} ...", accountsPath);
				var accountLoader = new AccountLoader(accountsPath, new AccountFactory());				
				var accounts = accountLoader.InitializeAccounts(out accountLoadErrors);

				Console.WriteLine("Loading transactions file {0} ...", transactionsPath);
				//Load Transactions
				var transactionLoader = new TransactionLoader(transactionsPath, accounts);
				var transactions = transactionLoader.TransformTransactions();

				Console.WriteLine("Processing Transactions ...");
				//Execute transactions
				var transactionProcessor = new TransactionProcessor(accounts, transactions);
				var processedTransactions = transactionProcessor.ProcessTransactions();
				
				//Get the failed transactions for error reporting
				failedTransactions = processedTransactions.Where(t => !t.TransactionStatus.TransactionSucceeded).ToList();

				Console.WriteLine("Outputing finished accounts to {0} ...", postExecutionAccountsPath);
				//Output the finished state of accounts
				var accountOutputRecorder = new AccountRecorder(postExecutionAccountsPath, accounts);
				accountOutputRecorder.RecordAccounts();
			}
			catch (Exception ex)
			{
				//Here I'm letting any errors just bubble up to this main program execution
				//so we can record what went wrong above. If more time permitted, would have 
				//probably added more error handling inside execution classes.

				Console.WriteLine("Application error encountered, see error file for details.");
				applicationErrorMessages.Add(ex.Message);
			}
			
			try
			{
				Console.WriteLine("Writing error logs and failed transactions if any to file to {0}.", errorPath);
				ErrorRecorder errorRecorder = new ErrorRecorder(errorPath, failedTransactions, accountLoadErrors, applicationErrorMessages);
				errorRecorder.RecordErrors();
			}
			catch (Exception ex)
			{
				Console.WriteLine(
					string.Format("Critical failure in the application - unable to write out error log: {0}", 
					ex.Message));
			}

			Console.WriteLine("Successful execution of the AccountExecutor. See your output paths for results.");
			Console.ReadKey();
		}
	}
}
