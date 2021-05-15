using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM_Contact_Service.Data;
using CRM_Contact_Service.Transfer;
using CRM_Contact_Service.Converter;

namespace CRM_Contact_Service.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly ContactServiceContext _context;

        public ContactInfoController(ContactServiceContext context)
        {
            _context = context;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactInfoResponse>>> GetContactInfoDAO()
        {
            List<ContactInfoDAO> contactInfos = await _context.ContactInfoDAO.ToListAsync();
            List<ContactInfoResponse> responseContacts = new List<ContactInfoResponse>();

            foreach(ContactInfoDAO contact in contactInfos)
            {
                responseContacts.Add(ContactInfoConverter.DAOToResponse(contact));
            }

            return Ok(responseContacts);
        }

        // GET: api/contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInfoResponse>> GetContactInfoDAO(long id)
        {
            var contactInfoDAO = await _context.ContactInfoDAO.FindAsync(id);

            if (contactInfoDAO == null)
            {
                return NotFound();
            }

            return Ok(ContactInfoConverter.DAOToResponse(contactInfoDAO));
        }

        // PUT: api/contacts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactInfoDAO(long id, ContactInfoRequest contactInfoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!ContactInfoDAOExists(id))
            {
                return NotFound();
            }

            ContactInfoDAO contactInfoDAO = await _context.ContactInfoDAO.FindAsync(id);
            ContactInfoConverter.RequestToExistingDAO(contactInfoRequest, contactInfoDAO);
            _context.Entry(contactInfoDAO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        // POST: api/contacts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ContactInfoDAO>> PostContactInfoDAO(ContactInfoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ContactInfoDAO contactInfoDAO = ContactInfoConverter.RequestToDAO(request);

            _context.ContactInfoDAO.Add(contactInfoDAO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactInfoDAO", new { id = contactInfoDAO.ContactInfoId }, contactInfoDAO);
        }

        // DELETE: api/contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactInfoDAO>> DeleteContactInfoDAO(long id)
        {
            var contactInfoDAO = await _context.ContactInfoDAO.FindAsync(id);

            if (contactInfoDAO == null)
            {
                return NotFound();
            }

            _context.ContactInfoDAO.Remove(contactInfoDAO);
            await _context.SaveChangesAsync();

            return contactInfoDAO;
        }

        private bool ContactInfoDAOExists(long id)
        {
            return _context.ContactInfoDAO.Any(e => e.ContactInfoId == id);
        }
    }
}
