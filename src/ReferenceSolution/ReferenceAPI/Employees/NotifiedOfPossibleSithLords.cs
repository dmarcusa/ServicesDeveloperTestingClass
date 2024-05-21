namespace ReferenceAPI.Employees;

public class NotifiedOfPossibleSithLords(ILogger<NotifiedOfPossibleSithLords> logger) : INotifyOfPossibleSithLords
{
    public void Notify(string firstName, string lastName)
    {
        logger.LogInformation("We Have a Possible Sith Lord {first} {last}", firstName, lastName);
    }
}
