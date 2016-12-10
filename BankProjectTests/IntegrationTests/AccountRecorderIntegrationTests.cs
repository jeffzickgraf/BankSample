using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankProject.Output;
using BankProject.Accounts;
using System.Collections.Generic;
using BankProject.Accounts.AccountRules;

namespace BankProjectTests.IntegrationTests
{
	[TestClass]
	public class AccountRecorderIntegrationTests
	{
		[TestMethod]
		public void ShouldCreateAccountRecordFile()
		{
			IAccount businessAccount = new BusinessAccount(7, "Jeff Business", 100.00m, new BusinessAccountTransactionRules());
			IAccount personalAccount = new PersonalAccount(2, "Lisa Personal", 20.00m, new PersonalAccountTransactionRules());
			IList<IAccount> accounts = new List<IAccount>() { businessAccount, personalAccount };

			AccountRecorder recorder = new AccountRecorder(@"C:\Source\InGen\TestAccountOutput\accountsOut.dat", accounts);
			recorder.RecordAccounts();
		}
	}
}
