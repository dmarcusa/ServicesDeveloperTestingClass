using Marten;
using ReferenceAPI.Employees;

namespace ReferenceApi.Employees;

public class EmployeeUniquenessChecker(IQuerySession session) : ICheckForUniqueEmployeeStubs
{
    public async Task<bool> CheckUniqueAsync(string slug, CancellationToken token)
    {
        return await session.Query<EmployeeEntity>().Where(e => e.Slug == slug).AnyAsync() == false;
    }
}