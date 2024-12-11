﻿namespace AccountLookupAPI.Interfaces
{
    public interface IAddress
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        string Country { get; set; }
    }
}
