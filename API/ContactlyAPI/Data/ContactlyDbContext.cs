/**
*  Purpose: This classes purpose is to talk between the contacts web application and the database we have. Fetching / adding data goes through here as a medium (.NET layer)
*
*/
using System;
using ContactlyAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContactlyAPI.Data;

public class ContactlyDbContext : DbContext
{
    public ContactlyDbContext(DbContextOptions options) : base(options)
    {

    }

    // each property represents a table inside the database
    public DbSet<Contact> Contacts { get; set; }


}
