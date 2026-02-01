// DTO = a data transfer object, this means it carries data between processes - from webapp with specified values to Context

using System;

namespace ContactlyAPI.Models;

public class AddContactRequestDTO
{
    public required string Name { get; set; } // name of contact
    public string? Email { get; set; } // email of contact - question mark means [optional], null or string type - opposite of required
    public required string Phone { get; set; }
    public bool Favourite { get; set; }

}
