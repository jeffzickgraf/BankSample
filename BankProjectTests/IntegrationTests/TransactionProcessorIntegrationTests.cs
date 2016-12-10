using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BankProject.Accounts;
using System.Collections.Generic;
using BankProject.Transaction;
using System.Reflection;
using System.Linq;

namespace BankProjectTests.IntegrationTests
{
	[TestClass]
	public class TransactionProcessorIntegrationTests
	{
		[TestMethod]
		public void ShouldProcessTransactions()
		{			
			string accountPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\accounts.dat");
			AccountLoader accountLoader = new AccountLoader(accountPath, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = accountLoader.InitializeAccounts(out errors);

			string transactionPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\4ErrantTransactions.dat");
			//Act
			TransactionLoader transactionLoader = new TransactionLoader(transactionPath, accounts);

			IList<ITransaction> transactions = transactionLoader.TransformTransactions();

			TransactionProcessor transactionProcessor = new TransactionProcessor(accounts, transactions);
			transactionProcessor.ProcessTransactions();
			Assert.IsTrue(transactionProcessor.Transactions.Count == 499);
			IList<ITransaction> failedTransactions 
				= transactionProcessor.Transactions.Where(t => !t.TransactionStatus.TransactionSucceeded).ToList();
			Assert.AreEqual(13, failedTransactions.Count());

		}
	}
}
