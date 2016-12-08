using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Transaction
{
	public class TransactionDataReader
	{
		public TransactionDataReader(string transactionsFilePath)
		{
			var lines = File.ReadAllLines(transactionsFilePath).Select(csv => csv.Split(','));
		}
	}
}
