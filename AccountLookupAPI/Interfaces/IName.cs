namespace AccountLookupAPI.Interfaces
{
    public interface IName
    {
        string Title { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Suffix { get; set; }
    }

}
