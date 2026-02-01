using ContactlyAPI.Data;
using ContactlyAPI.Models;
using ContactlyAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ContactlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlyDbContext dbContext; // readonly means it is immutable

        // constructor injection - dependecy injection, handles an instance of the dbContext class
        public ContactsController(ContactlyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // writing endpoint of the database
        [HttpGet]
        public IActionResult GetAllContacts() // getting all the contacts - interface method
        {
            var contacts = dbContext.Contacts.ToList(); // gettin the list of contacts and making them a list

            return Ok(contacts); // okay response (200) and returns the contacts
        }

        [HttpPost]
        public IActionResult AddContact(AddContactRequestDTO request)
        {
            // mapping request from DTO to the domain model of type contact
            var domainModelContact = new Contact
            {
                Id = Guid.NewGuid(), // creates a new unique ID automatically - built in func
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favourite = request.Favourite
            };

            dbContext.Contacts.Add(domainModelContact);// adding the new Contact to the Contacts colection found in DbContext
            dbContext.SaveChanges(); // saving is required as it pushes the contact after it has been added - think of commit and push in Git

            return Ok(domainModelContact);
        }

        [HttpDelete]
        [Route("{id:Guid}")] // getting the id (type Guid) identifier from the route 
        public IActionResult DeleteContact(Guid id)
        {
            var contact = dbContext.Contacts.Find(id); // finding to see if the contact exists

            if (contact != null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
