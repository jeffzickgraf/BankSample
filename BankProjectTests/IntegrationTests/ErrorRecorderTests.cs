using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using BankProject.Accounts;
using BankProject.Transaction;
using System.Linq;
using BankProject.Output;

namespace BankProjectTests.IntegrationTests
{
	/// <summary>
	/// Summary description for ErrorRecorderTests
	/// </summary>
	[TestClass]
	public class ErrorRecorderTests
	{		
		[TestMethod]
		public void ShouldCreateTestErrorFile()
		{
			IList<ITransaction> failedTransactions = GetSomeFailedTransactions();
			IList<AccountLoadError> accountErrors = GetSomeAccountErrors();
			IList<string> appErrors = new List<string>() { "something in application errored" };

			ErrorRecorder recorder = new ErrorRecorder(@"C:\Source\InGen\ErrorOutput\errors.csv", failedTransactions, accountErrors, appErrors);
			recorder.RecordErrors();
		}

		private IList<AccountLoadError> GetSomeAccountErrors()
		{
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\partialerrantaccounts.dat");
			AccountLoader loader = new AccountLoader(path, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = loader.InitializeAccounts(out errors);
			return errors;
		}

		private IList<ITransaction> GetSomeFailedTransactions()
		{
			string accountPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\accounts.dat");
			AccountLoader accountLoader = new AccountLoader(accountPath, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = accountLoader.InitializeAccounts(out errors);

			string transactionPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\4ErrantTransactions.dat");

			TransactionLoader transactionLoader = new TransactionLoader(transactionPath, accounts);

			IList<ITransaction> transactions = transactionLoader.TransformTransactions();
			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			return transactions.Where(t => !t.TransactionStatus.TransactionSucceeded).ToList();
		}
	}
}
