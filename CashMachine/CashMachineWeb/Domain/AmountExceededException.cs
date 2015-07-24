using System;

namespace CashMachineWeb.Domain
{
	public class AmountExceededException : Exception
	{
		public AmountExceededException(string message) : base(message)
		{
		}
	}
}