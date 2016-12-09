namespace BankProject.Accounts
{
	/// <summary>
	/// Represents a factory to create accounts.
	/// </summary>
	public interface IAccountFactory
	{
		/// <summary>
		/// Creates an account based on account type and assigns any needed values to the account for initialization.
		/// </summary>
		/// <param name="accountType">The account type.</param>
		/// <param name="accountNumber">The account number.</param>
		/// <param name="accountOwner">The account owner.</param>
		/// <param name="initialBalance">The initial balance.</param>
		/// <returns>Creates a concrete type account based on: <see cref="AccountBase"/></returns>
		AccountBase CreateAccount(string accountType, int accountNumber, string accountOwner, decimal initialBalance);
	}
}