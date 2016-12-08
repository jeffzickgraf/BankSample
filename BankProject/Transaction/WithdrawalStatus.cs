using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Transaction
{
    /// <summary>
    /// Indication of capability to withdrawal funds from account.
    /// </summary>
    public class WithdrawalStatus
    {
		public WithdrawalStatus(bool succeeded, string failureReason)
		{
			WithdrawalSucceeded = succeeded;
			FailureReason = failureReason;
		}
		
		/// <summary>
		/// Indicates if the withdrawal was successful.
		/// </summary>
        public bool WithdrawalSucceeded { get; set; }

		/// <summary>
		/// If the withdrawal failed, the reason why.
		/// </summary>
        public string FailureReason { get; set; }
    }
}
