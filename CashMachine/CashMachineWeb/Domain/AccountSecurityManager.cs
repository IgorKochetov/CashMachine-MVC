using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public class AccountSecurityManager : IAccountSecurityManager
	{
		private const byte IncorrectNumberOfPinInputsAllowed = 3;
		public void ProcessIncorrectPinInput(CreditCardAccount account)
		{
			account.IncorrectPinInputCounter++;
			if (account.IncorrectPinInputCounter > IncorrectNumberOfPinInputsAllowed)
			{
				account.IsBlocked = true;
			}
		}
	}
}