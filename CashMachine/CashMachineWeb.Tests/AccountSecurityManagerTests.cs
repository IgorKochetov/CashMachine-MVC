using CashMachineWeb.Domain;
using CashMachineWeb.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CashMachineWeb.Tests
{
	[TestFixture]
	public class AccountSecurityManagerTests
    {
		[Test]
		public void DefaultAllowedNumber_ShouldBeEqualToThree()
		{
			// arrange
			var sut = new AccountSecurityManager();
			// act & assert
			sut.IncorrectNumberOfPinInputsAllowed.ShouldBeEquivalentTo(3);
		}

		[TestCase(0, 1)]
		[TestCase(1, 2)]
		[TestCase(4, 5)]
		public void ProcessIncorrectPinInput_ShouldIncreaseCounterByOne(byte initialValue, byte expectedValue)
		{
			// arrange
			var sut = new AccountSecurityManager();
			CreditCardAccount account = new CreditCardAccount { IncorrectPinInputCounter = initialValue };
			// act
			sut.ProcessIncorrectPinInput(account);
			// assert
			account.IncorrectPinInputCounter.ShouldBeEquivalentTo(expectedValue);
		}

		[TestCase(1, 2, false)]
		[TestCase(2, 2, true)]
		[TestCase(3, 2, true)]
		public void ProcessIncorrectPinInput_CounterExceedsAllowed_ShouldBlockAccount_(byte counter, byte allowed, bool shouldBeBlocked)
		{
			// arrange
			var sut = new AccountSecurityManager(allowed);
			CreditCardAccount account = new CreditCardAccount { IncorrectPinInputCounter = counter };
			// act
			sut.ProcessIncorrectPinInput(account);
			// assert
			account.IsBlocked.ShouldBeEquivalentTo(shouldBeBlocked);
		}
    }
}
