using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using BankProject.Transaction;
using BankProject.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace BankProjectTests
{
	/*
	More of an integration test since this depends on outside files.
	*/
	[TestClass]
	public class TransactionLoaderTests
	{	
		[TestMethod]
		public void ShouldLoadTransactions()
		{
			//Arrange
			string accountPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\accounts.dat");
			AccountLoader accountLoader = new AccountLoader(accountPath, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = accountLoader.InitializeAccounts(out errors);

			string transactionPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\transactions.dat");
			//Act
			TransactionLoader transactionLoader = new TransactionLoader(transactionPath, accounts);

			IList<ITransaction> transactions = transactionLoader.TransformTransactions();
			Assert.IsTrue(transactions.Count == 499);
			Assert.IsTrue(transactions.Where(x => x.IsTransactable).Count() == 499);

		}

		[TestMethod]
		public void ShouldLoadTransactionsWith4Errant()
		{
			//Arrange
			string accountPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\accounts.dat");
			AccountLoader accountLoader = new AccountLoader(accountPath, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = accountLoader.InitializeAccounts(out errors);

			string transactionPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\4ErrantTransactions.dat");
			//Act
			TransactionLoader transactionLoader = new TransactionLoader(transactionPath, accounts);

			IList<ITransaction> transactions = transactionLoader.TransformTransactions();
			Assert.IsTrue(transactions.Count == 499);
			Assert.IsTrue(transactions.Where(x => x.IsTransactable).Count() == 499 - 4);

		}

	}
}
