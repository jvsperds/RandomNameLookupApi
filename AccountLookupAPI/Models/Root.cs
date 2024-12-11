using System.Xml.Serialization;

namespace AccountLookupAPI.Models
{
    [XmlRoot("Root")]
    public class Root
    {
        [XmlElement("LookupResponse")]
        public LookupResponse? LookupResponse { get; set; }
    }

    public class LookupResponse
    {
        [XmlElement("LookupResults")]
        public ResultWrapper? LookupResults { get; set; }
    }

    [XmlType("LookupResults")]
    public class ResultWrapper
    {
        [XmlElement("LookupResult")]
        public List<LookupResult> LookupResults { get; set; } = new(); // Ensure default initialization

        public ResultWrapper() { }

        public ResultWrapper(List<LookupResult> lookupResults)
        {
            LookupResults = lookupResults;
        }
    }

    [XmlType("LookupResult")]
    public class LookupResult
    {
        [XmlElement("AccountNumber", IsNullable = true)]
        public string AccountNumber { get; set; } = "";

        [XmlElement("Title", IsNullable = true)]
        public string Title { get; set; } = "";

        [XmlElement("FirstName", IsNullable = true)]
        public string FirstName { get; set; } = "";

        [XmlElement("MiddleName", IsNullable = true)]
        public string MiddleName { get; set; } = "";

        [XmlElement("LastName", IsNullable = true)]
        public string LastName { get; set; } = "";

        [XmlElement("Suffix", IsNullable = true)]
        public string Suffix { get; set; } = "";

        [XmlElement("Address1", IsNullable = true)]
        public string Address1 { get; set; } = "";

        [XmlElement("Address2", IsNullable = true)]
        public string Address2 { get; set; } = "";

        [XmlElement("City", IsNullable = true)]
        public string City { get; set; } = "";

        [XmlElement("State", IsNullable = true)]
        public string State { get; set; } = "";

        [XmlElement("Zip", IsNullable = true)]
        public string Zip { get; set; } = "";

        [XmlElement("Country", IsNullable = true)]
        public string Country { get; set; } = "";

        [XmlElement("ReturnCode")]
        public int ReturnCode { get; set; }

        [XmlElement("ErrorMessage", IsNullable = true)]
        public string ErrorMessage { get; set; } = "";
    }


    [XmlRoot("ExactLookupRequest")]
    public class ExactLookupRequest
    {
        [XmlElement("AccountNumber")]
        public string? AccountNumber { get; set; }
    }

    [XmlRoot("WildLookupRequest")]
    public class WildLookupRequest
    {
        [XmlElement("FirstName")]
        public string? FirstName { get; set; }

        [XmlElement("LastName")]
        public string? LastName { get; set; }

        [XmlElement("City")]
        public string? City { get; set; } = "";

        [XmlElement("State")]
        public string? State { get; set; } = "";

        [XmlElement("Zip")]
        public string? Zip { get; set; } = "";
    }
}
