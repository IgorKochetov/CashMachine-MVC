using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public interface IAccountSecurityManager
	{
		void ProcessIncorrectPinInput(CreditCardAccount account);
	}
}