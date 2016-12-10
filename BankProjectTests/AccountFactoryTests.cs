using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankProject.Accounts;
using BankProject.Accounts.AccountRules;

namespace BankProjectTests
{
	[TestClass]
	public class AccountFactoryTests
	{
		[TestMethod]
		public void ShouldCreatePersonalAccount()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("Personal", 123, "Jeff Zickgraf", 1000.00m);
			Assert.IsInstanceOfType(account, typeof(PersonalAccount), "Unexpected instance for account type.");
		}

		[TestMethod]
		public void ShouldCreateBusinessAccount()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("Business", 123, "Jeff Zickgraf", 1000.00m);
			Assert.IsInstanceOfType(account, typeof(BusinessAccount), "Unexpected instance for account type.");
		}

		[TestMethod]
		public void ShouldCreatePersonalItializedCorrectly()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("Personal", 123, "Jeff Zickgraf", 1000.00m);
			Assert.IsInstanceOfType(account.AccountRules, typeof(PersonalAccountTransactionRules), "Wrong transaction rule assigned.");
			Assert.AreEqual(123, account.AccountNumber, "Unexpected account number.");
			Assert.AreEqual(1000.00m, account.Balance, "Unexpected initialized balance.");
		}

		[TestMethod]
		public void ShouldCreateBusinessAccountItializedCorrectly()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("Business", 123, "Jeff Zickgraf", 1000.00m);
			Assert.IsInstanceOfType(account.AccountRules, typeof(BusinessAccountTransactionRules), "Wrong transaction rule assigned.");
			Assert.AreEqual(123, account.AccountNumber, "Unexpected account number.");
			Assert.AreEqual(1000.00m, account.Balance, "Unexpected initialized balance.");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShouldErrorWithUncapitalizedPersonalAccountType()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("personal", 123, "Jeff Zickgraf", 1000.00m);
			
			//Assert is an expected exception
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShouldErrorWithUncapitalizedBusinessAccountType()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("business", 123, "Jeff Zickgraf", 1000.00m);

			//Assert is an expected exception
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShouldErrorWithUnknownAccountType()
		{
			AccountFactory target = new AccountFactory();
			var account = target.CreateAccount("Investment", 123, "Jeff Zickgraf", 1000.00m);

			//Assert is an expected exception
		}
	}
}
