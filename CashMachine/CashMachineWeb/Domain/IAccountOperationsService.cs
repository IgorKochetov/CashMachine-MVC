using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public interface IAccountOperationsService
	{
		AccountBalanceStamp GetBalanceForAccount(string accountNumber);
		OperationResult WithdrawMoney(string accountNumber, decimal withdrawAmount);
	}
}
