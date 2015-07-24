using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public class AccountSecurityManager : IAccountSecurityManager
	{
		private readonly byte incorrectNumberOfPinInputsAllowed;

		public AccountSecurityManager(byte incorrectNumberOfPinInputsAllowed)
		{
			this.incorrectNumberOfPinInputsAllowed = incorrectNumberOfPinInputsAllowed;
		}

		public AccountSecurityManager() : this (incorrectNumberOfPinInputsAllowed: 3)
		{
		}

		public byte IncorrectNumberOfPinInputsAllowed
		{
			get { return incorrectNumberOfPinInputsAllowed; }
		}

		public void ProcessIncorrectPinInput(CreditCardAccount account)
		{
			account.IncorrectPinInputCounter++;
			if (account.IncorrectPinInputCounter > incorrectNumberOfPinInputsAllowed)
			{
				account.IsBlocked = true;
			}
		}

		public void ProcessCorrectPinInput(CreditCardAccount account)
		{
			account.IncorrectPinInputCounter = 0;
		}
	}
}