using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankProject.Accounts;
using BankProject.Output;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BankProjectTests
{
	[TestClass]
	public class AccountRecorderTests
	{
		[TestMethod]
		public void ConstructorShouldOrderByAccountLeastToGreatest()
		{
			const int LOWEST = 1;
			const int SECONDLOWEST = 5;
			const int HIGHEST = 100;

			var lowAccountMock = new Mock<IAccount>();
			lowAccountMock.Setup(account => account.AccountNumber).Returns(LOWEST);

			var secondLowestAccountMock = new Mock<IAccount>();
			secondLowestAccountMock.Setup(account => account.AccountNumber).Returns(SECONDLOWEST);

			var highAccountMock = new Mock<IAccount>();
			highAccountMock.Setup(account => account.AccountNumber).Returns(HIGHEST);
						
			IList<IAccount> accounts = new List<IAccount>() {  highAccountMock.Object, lowAccountMock.Object, secondLowestAccountMock.Object };
			AccountRecorder recorder = new AccountRecorder("x", accounts);

			Assert.IsTrue(recorder.Accounts.First().AccountNumber == LOWEST);
			Assert.IsTrue(recorder.Accounts.Last().AccountNumber == HIGHEST);
		}
	}
}
