namespace Prosigliere.Blog.Api;

/// <summary>
/// Represents the result of validation errors in the application layer
/// A Dictionary with a propertyName as a key and an array of errors for that given property
/// </summary>
public class Errors : Dictionary<string, string[]>
{
    public Errors(IDictionary<string, string[]> dictionary) : base(dictionary)
    {
    }
}