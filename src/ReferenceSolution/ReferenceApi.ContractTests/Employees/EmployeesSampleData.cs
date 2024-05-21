using ReferenceAPI.Employees;
using System.Collections;

namespace ReferenceApi.ContractTests.Employees;
public class EmployeesSampleData
{
    private readonly IReadOnlyList<object[]> _data = [
        [new EmployeeCreateRequest { FirstName = "Robert", LastName="Smith"}, "smith-robert"],
        [new EmployeeCreateRequest { FirstName = "Johnny", LastName="Marr"}, "marr-jonny"],
        [new EmployeeCreateRequest { FirstName = "   Bob     ", LastName="  Mould"}, "mould-bob"],
        ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
