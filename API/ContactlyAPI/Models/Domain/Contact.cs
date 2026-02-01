using System;

namespace ContactlyAPI.Models.Domain;

public class Contact
{
    public Guid Id { get; set; } // unique id of contact
    public required string Name { get; set; } // name of contact
    public string? Email { get; set; } // email of contact - question mark means [optional], null or string type - opposite of required
    public required string Phone { get; set; }
    public bool Favourite { get; set; }

}


