using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public interface IAccountOperationsService
	{
		BalanceModel GetBalanceForAccount(string accountNumber);
		OperationResult WithdrawMoney(string accountNumber, decimal withdrawAmount);
	}
}
