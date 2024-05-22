using Banking;
using NSubstitute;

namespace LearningDeveloperTest.BankAccount;
public class MakingWithdraws
{
    //will be one create for each method if not use fixture
    private readonly Account account;

    public MakingWithdraws()
    {
        account = new Account(Substitute.For<ICalculateBonuses>(), Substitute.For<IProvideCustomerRelations>());
    }

    [Theory]
    [InlineData(25.23)]
    [InlineData(6000)]
    public void MakingWithrawsDecreasesBalance(decimal amountToWithdraw)
    {
        var openingBalance = account.GetBalance();

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact]
    public void OverdraftNotAllowed()
    {
        var openingBalance = account.GetBalance();

        Assert.Throws<AccountOverdraftException>(() => account.Withdraw(openingBalance + .01M));
        Assert.Equal(openingBalance, account.GetBalance());
    }

    [Fact]
    public void CanWithdrawFullAmount()
    {
        account.Withdraw(account.GetBalance());

        Assert.Equal(0, account.GetBalance());
    }
}
