
using Banking;
using NSubstitute;

namespace LearningDeveloperTest.BankAccount;
public class NewAccountTest
{

    [Fact]
    public void NewAccountsHaveCorrectOpeningBalance()
    {
        //fakes below where it returns the default for decimals
        var account = new Account(Substitute.For<ICalculateBonuses>(), Substitute.For<IProvideCustomerRelations>());
        //var account = new Account(new DummyBonusCalculator());
        decimal openingBalance = account.GetBalance();

        Assert.Equal(7000M, openingBalance);
    }
}

public class DummyBonusCalculator : ICalculateBonuses
{
    public decimal HowMuchBonusFor(decimal balance, decimal amountToDeposit)
    {
        return 0;
    }
}
