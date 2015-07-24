using System;
using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	class AccountOperationsServiceStub : IAccountOperationsService
	{
		public BalanceModel GetBalanceForAccount(string accountNumber)
		{
			return new BalanceModel
			{
				CardNumber = accountNumber,
				MoneyAmount = 1000000,
				Timestamp = DateTime.Now
			};
		}

		public OperationResult WithdrawMoney(string accountNumber, decimal withdrawAmount)
		{
			return new OperationResult
			{
				AmountWithdrawed = withdrawAmount,
				BalanceLeftAmount = 10000,
				CardNumber = accountNumber,
				Timestamp = DateTime.Now
			};
		}
	}
}