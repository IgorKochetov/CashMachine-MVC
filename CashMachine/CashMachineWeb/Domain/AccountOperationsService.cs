using System;
using System.Linq;
using CashMachineWeb.Database;
using CashMachineWeb.Models;

namespace CashMachineWeb.Domain
{
	public class AccountOperationsService : IAccountOperationsService
	{
		// it is actually should be abstract repository of CreditCardAccounts
		// so we can effectively test our module without direct dependencies on the EntityFramework infrastructure
		// but we cannot do it right now because after choosing AspNet.Identity as our component to work with Users/Accounts
		// (in order to save time developing custom solution) we are now tied to EF via IdentityUser we derive our CreditCardAccount from
		private readonly CashMachineDbContext dbContext;
		public AccountOperationsService(CashMachineDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public BalanceModel GetBalanceForAccount(string accountNumber)
		{
			var account = dbContext.Users
				.Single(a => a.UserName == accountNumber);
			return new BalanceModel
			{
				CardNumber = accountNumber,
				MoneyAmount = account.Balance,
				Timestamp = DateTime.Now,
			};
		}

		public OperationResult WithdrawMoney(string accountNumber, decimal withdrawAmount)
		{
			var account = dbContext.Users
				.Single(a => a.UserName == accountNumber);

			// in a real system we would be using transactions here to try to withdraw money
			// and then check if successful (and amount was not exceeded)
			// in order to avoid possible concurrency issues
			// here we will stick with naive approach of just checking beforehand and process the results

			if (withdrawAmount > account.Balance)
			{
				throw new AmountExceededException("Can't proceed withdrawal. Not enough balance left!");
			}

			account.Balance -= withdrawAmount;
			dbContext.SaveChanges();

			return new OperationResult
			{
				CardNumber = accountNumber,
				Timestamp = DateTime.Now,
				BalanceLeftAmount = account.Balance,
				AmountWithdrawed = withdrawAmount
			};
		}
	}
}