using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankProject.Accounts;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BankProjectTests
{
	[TestClass]
	public class AccountLoaderIntegrationTests
	{
		/*
		More of an integration test since this depends on outside files.
		*/

		[TestMethod]
		public void ShouldInitializeAccountsWithNoErrors()
		{
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\accounts.dat");
			AccountLoader loader = new AccountLoader(path, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = loader.InitializeAccounts(out errors);
			Assert.IsTrue(accounts.Count > 0);
		}

		public void ShouldInitializeAccountsWith5Errors()
		{
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestResources\partialerrantaccounts.dat");
			AccountLoader loader = new AccountLoader(path, new AccountFactory());
			IList<AccountLoadError> errors;
			IList<IAccount> accounts = loader.InitializeAccounts(out errors);
			Assert.IsTrue(accounts.Count == 5);
		}
	}
}
