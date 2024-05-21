
namespace ReferenceAPI.Employees;

public class EmployeeSlugGenerator
{
    public string Generate(string firstName, string? lastName)
    {
        //if (string.IsNullOrEmpty(lastName))
        //{
        //    return firstName.ToLowerInvariant();
        //}
        //return $"{Clean(lastName)}-{Clean(firstName)}";

        var slug = (Clean(firstName), Clean(lastName)) switch
        {
            (string first, null) => first,
            (string first, string last) => $"{last}-{first}",
            _ => throw new InvalidOperationException() // Chaos
        };

        bool isUnique = _uniquessChecker.CheckForUniqueSlug(slug);
        return slug;
    }

    //Never type private, always refector to it
    private static string? Clean(string? part)
    {
        if (string.IsNullOrWhiteSpace(part))
        {
            return null;
        }
        return part.ToLowerInvariant().Trim();
    }

}
