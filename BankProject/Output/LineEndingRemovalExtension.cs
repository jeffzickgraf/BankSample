using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Output
{
	/// <summary>
	/// Remove line endings from a string.
	/// </summary>
	public static class LineEndingRemovalExtension
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
	}
}
