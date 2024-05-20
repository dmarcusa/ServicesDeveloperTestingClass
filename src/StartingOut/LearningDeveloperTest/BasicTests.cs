namespace LearningDeveloperTest;
public class BasicTests
{
    [Fact]
    public void CanAddTenAndTwenty()
    {
        // Given (Arrange) - this should recreate the world from scratch
        int a = 10, b = 20;
        int c;

        //The important part - what we are actually testing.
        //WHEN (Act)
        c = a + b; //System Under Test (SUT)

        //Then (Assert)
        Assert.Equal(30, c);
    }

    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(4, 2, 6)]
    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        int answer = a + b;

        Assert.Equal(expected, answer);

    }
}
