using Banking;
using NSubstitute;

namespace LearningDeveloperTest.BankAccount;
public class MakingDeposits
{
    [Theory]
    [InlineData(100)]
    public void MakingDepositsOnAccountsIncreaseBalance(decimal amountToDeposit)
    {
        var account = new Account(Substitute.For<ICalculateBonuses>(), Substitute.For<IProvideCustomerRelations>());
        var openingBalance = account.GetBalance();

        account.Deposit(amountToDeposit);

        Assert.Equal(openingBalance + amountToDeposit, account.GetBalance());
    }
}
