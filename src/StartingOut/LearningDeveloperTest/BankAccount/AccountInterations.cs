
using Banking;
using NSubstitute;

namespace LearningDeveloperTest.BankAccount;
public class AccountInterations
{
    [Fact]
    public void AccountUsesTheBonusCalculator()
    {
        var stubbedBonusCalculator = Substitute.For<ICalculateBonuses>();
        var account = new Account(stubbedBonusCalculator, Substitute.For<IProvideCustomerRelations>());
        var openingBalance = account.GetBalance();
        var amountToDeposit = 113.82M;
        stubbedBonusCalculator.HowMuchBonusFor(openingBalance, amountToDeposit).Returns(420.69M);

        account.Deposit(amountToDeposit);

        Assert.Equal(openingBalance + amountToDeposit + 420.69M, account.GetBalance());
    }

    [Fact]
    public void NotifiesOfZeroBalanceOnWithdrawal()
    {
        var stubbedPr = Substitute.For<IProvideCustomerRelations>();
        var account = new Account(Substitute.For<ICalculateBonuses>(), stubbedPr);

        account.Withdraw(account.GetBalance());

        stubbedPr
            .Received(1)
            .SendEmailAboutBalanceBeingZero(account);
    }

    [Fact]
    public void NotNotifiedWhenBalanceIsAboutZero()
    {
        var stubbedPr = Substitute.For<IProvideCustomerRelations>();
        var account = new Account(Substitute.For<ICalculateBonuses>(), stubbedPr);

        account.Withdraw(account.GetBalance() - .01M);

        stubbedPr
            .DidNotReceive()
            .SendEmailAboutBalanceBeingZero(Arg.Any<Account>());
    }
}
