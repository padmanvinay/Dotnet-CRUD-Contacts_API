using System.Diagnostics.Contracts;
using System.Net;
using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute] Guid id)
        {
            var contacts = await dbContext.Contacts.FindAsync(id);
            if(id != null)
            {
                return Ok(contacts);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<IActionResult> AddContacts(AddcontactRequest addcontactRequest)

        {
            var contact = new Contact()
            {
                id = Guid.NewGuid(),
                address = addcontactRequest.address,
                fullName = addcontactRequest.fullName,
                email = addcontactRequest.email,
                phone = addcontactRequest.phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }
        

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                contact.address=updateContactRequest.address;
                contact.email=updateContactRequest.email;
                contact.phone=updateContactRequest.phone;
                contact.fullName=updateContactRequest.fullName;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contacts = await dbContext.Contacts.FindAsync(id);
            if(id != null)
            {
                dbContext.Remove(contacts);
                await dbContext.SaveChangesAsync();
                return Ok(contacts);
            }
            return NotFound();
        }
    }
}