using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankProject.Transaction;
using BankProject.Accounts;
using BankProject.Accounts.AccountRules;
using System.Collections.Generic;

namespace BankProjectTests
{
	/*
	---Note: see ReadMe
	*/
	[TestClass]
	public class TransactionProcessorTests
	{
		[TestMethod]
		public void ShouldProcessTransferFromBusinessToPersonalCorrectly()
		{
			IAccount businessAccount = new BusinessAccount(1, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			ITransaction transaction = new Transaction(businessAccount, personalAccount, 10.00m, "x");
			IList<ITransaction> transactions = new List<ITransaction>() { transaction };

			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			Assert.AreEqual(89, businessAccount.Balance); //$10 transfer - $1 for fee
			Assert.AreEqual(30, personalAccount.Balance);
		}

		[TestMethod]
		public void ShouldChargeOverdraftFromBusinessWhenDropsBelowBalance()
		{
			IAccount businessAccount = new BusinessAccount(1, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			ITransaction transaction = new Transaction(businessAccount, personalAccount, 101.00m, "x");
			IList<ITransaction> transactions = new List<ITransaction>() { transaction };

			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			Assert.AreEqual(-22, businessAccount.Balance ); //$101 transfer - $20 overdraft - $1 fee
			Assert.AreEqual(121, personalAccount.Balance);
		}

		[TestMethod]
		public void ShouldRejectOverdraftFromBusinessWhenOverAllowance()
		{
			IAccount businessAccount = new BusinessAccount(1, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			ITransaction transaction = new Transaction(businessAccount, personalAccount, 101.00m, "x");
			ITransaction transaction2 = new Transaction(businessAccount, personalAccount, 1010.00m, "x");
			IList<ITransaction> transactions = new List<ITransaction>() { transaction, transaction2 };

			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			Assert.AreEqual(-22, businessAccount.Balance); //$101 transfer - $20 overdraft - $1 fee
			Assert.AreEqual(121, personalAccount.Balance);
			Assert.IsFalse(transaction2.TransactionStatus.TransactionSucceeded);
		}

		[TestMethod]
		public void ShouldAllowSeveralOverdraftsFromBusinessIfNotOverAllowance()
		{
			IAccount businessAccount = new BusinessAccount(1, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			ITransaction transaction = new Transaction(businessAccount, personalAccount, 101.00m, "x");
			ITransaction transaction2 = new Transaction(businessAccount, personalAccount, 100.00m, "x");
			ITransaction transaction3 = new Transaction(businessAccount, personalAccount, 100.00m, "x");
			IList<ITransaction> transactions = new List<ITransaction>() { transaction, transaction2, transaction3 };

			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			//$100 - $101 t1 - $100 t2 - $100 t3 - 3 * $20 overdraft - 3 * $1 fee
			Assert.AreEqual(-264, businessAccount.Balance); 
			Assert.AreEqual(321, personalAccount.Balance);
		}

		[TestMethod]
		public void ShouldRejectTransactionForOverdraftFromPersonalAccount()
		{
			IAccount businessAccount = new BusinessAccount(1, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			ITransaction transaction = new Transaction(personalAccount, businessAccount, 101.00m, "x");			
			IList<ITransaction> transactions = new List<ITransaction>() { transaction};

			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			Assert.AreEqual(100, businessAccount.Balance); 
			Assert.AreEqual(20, personalAccount.Balance);
			Assert.IsFalse(transaction.TransactionStatus.TransactionSucceeded);
		}

		[TestMethod]
		public void ShouldNotChargeFeeForOriginationFromPersonalAccount()
		{
			IAccount businessAccount = new BusinessAccount(1, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			ITransaction transaction = new Transaction(personalAccount, businessAccount, 5.00m, "x");
			IList<ITransaction> transactions = new List<ITransaction>() { transaction };

			TransactionProcessor processor = new TransactionProcessor(accounts, transactions);
			processor.ProcessTransactions();
			Assert.AreEqual(105, businessAccount.Balance);
			Assert.AreEqual(15, personalAccount.Balance);
			Assert.IsTrue(transaction.TransactionStatus.TransactionSucceeded);
		}

	}
}
