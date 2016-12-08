using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Transaction
{
    /// <summary>
    /// Indication of success or failure of a transaction and error reason if failure.
    /// </summary>
    public class TransactionStatus
    {
        public bool TransactionSucceeded { get; set; }
        public string Error { get; set; }

    }
}
