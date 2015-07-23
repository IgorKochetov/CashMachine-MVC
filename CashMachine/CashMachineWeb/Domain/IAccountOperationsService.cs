using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public interface IAccountOperationsService
	{
		BalanceModel GetBalanceForAccount(string accountNumber);
	}

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
	}
}
