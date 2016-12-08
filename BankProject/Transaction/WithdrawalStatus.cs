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
        public bool WithdrawalSucceeded { get; set; }
        public string FailureReason { get; set; }
    }
}
