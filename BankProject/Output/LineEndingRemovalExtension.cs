using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Output
{
	/// <summary>
	/// Extensions that can assist output of errors.
	/// </summary>
	public static class ErrorOutputExtensions
	{
		/// <summary>
		/// Remove line endings from string.
		/// </summary>
		/// <param name="value">value to remove from.</param>		
		public static string RemoveLineEndings(this string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return value;
			}
			string lineSeparator = ((char)0x2028).ToString();
			string paragraphSeparator = ((char)0x2029).ToString();

			return value
				.Replace("\r\n", string.Empty).Replace("\n", string.Empty)
				.Replace("\r", string.Empty)
				.Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
		}

		public static string ReplaceCommasForCSV(this string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return value;
			}

			return value.Replace(',', '|');
		}
	}
}
