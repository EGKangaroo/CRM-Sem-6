using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using CRM_Gateway.Schemas.ContactServiceSchemas;
using CRM_Gateway.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_Gateway.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactServiceController : ControllerBase
    {

        /// <summary>
        /// Gets a list of all contacts from the contact service
        /// </summary>
        /// <response code="200">returns the list of contacts</response>
        /// <response code="400">bad request, something went wrong on the client-side</response>
        /// <response code="500">processing error, something went wrong on the server-side</response>
        /// <returns>A list of contact infos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<ContactInfoResponse>>> GetAll()
        {
            IFlurlResponse response = await $"{URLConstants.ContactAPIURL}/api/contacts".GetAsync();

            if(response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if(response.StatusCode >= 400)
            {
                return StatusCode(400);
            }

            ICollection<ContactInfoResponse> responseObjects = await response.GetJsonAsync<ICollection<ContactInfoResponse>>();
            return Ok(responseObjects);
        }

        /// <summary>
        /// Gets a contact with the specified id
        /// </summary>
        /// <response code="200">returns a contact with the specified id</response>
        /// <response code="400">bad request, something went wrong on the client-side</response>
        /// <response code="404">no contact of that id was found</response>
        /// <response code="500">processing error, something went wrong on the server-side</response>
        /// <returns>A list of contact infos</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactInfoResponse>> GetById(long id)
        {
            IFlurlResponse response = await $"{URLConstants.ContactAPIURL}/api/contacts/{id}".GetAsync();

            if(response.StatusCode == 404)
            {
                return NotFound();
            }
            else if(response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if(response.StatusCode >= 400)
            {
                return StatusCode(400);
            }

            ContactInfoResponse responseObject = await response.GetJsonAsync<ContactInfoResponse>();
            return Ok(responseObject);
        }

        /// <summary>
        /// Creates a new contact
        /// </summary>
        /// <param name="requestObject">Contact request object</param>
        /// <returns>newly created contact</returns>
        /// <response code="201">a new contact has been created</response>
        /// <response code="400">bad request, something went wrong on the client-side</response>
        /// <response code="500">processing error, something went wrong on the server-side</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactInfoResponse>> Post(ContactInfoRequest requestObject)
        {
            IFlurlResponse response = await $"{URLConstants.ContactAPIURL}/api/contacts".PostJsonAsync(requestObject);

            if(response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if(response.StatusCode >= 400)
            {
                return StatusCode(400);
            }

            ContactInfoResponse responseObject = await response.GetJsonAsync<ContactInfoResponse>();
            return CreatedAtAction("Post", responseObject);
        }

        /// <summary>
        /// Updates an existing contact
        /// </summary>
        /// <param name="id">The id of the contact to alter</param>
        /// <param name="requestObject">Contact request object</param>
        /// <returns>No Content</returns>
        /// <response code="204">Update succesful, no content</response>
        /// <response code="404">Did not find the contact with the specified id</response>
        /// <response code="400">bad request, something went wrong on the client-side</response>
        /// <response code="500">processing error, something went wrong on the server-side</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(long id, ContactInfoRequest requestObject)
        {
            IFlurlResponse response = await $"{URLConstants.ContactAPIURL}/api/contacts/{id}".PutJsonAsync(requestObject);

            if(response.StatusCode == 404)
            {
                return NotFound();
            }
            else if(response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if(response.StatusCode >= 400)
            {
                return StatusCode(400);
            }

            return StatusCode(204);
        }

        /// <summary>
        /// deletes a contact with a specific id
        /// </summary>
        /// <param name="id">the id of the contact to be deleted</param>
        /// <returns>Status code of delete request</returns>
        /// <response code="200">The contact is successfully deleted</response>
        /// <response code="404">No contact found with the matching id</response>
        /// <response code="400">bad request, something went wrong on the client-side</response>
        /// <response code="500">processing error, something went wrong on the server-side</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            IFlurlResponse response = await $"{ URLConstants.ContactAPIURL }/api/contacts/{id}".DeleteAsync();

            if (response.StatusCode == 404)
            {
                return NotFound();
            }
            else if (response.StatusCode >= 500)
            {
                return StatusCode(500);
            }
            else if (response.StatusCode >= 400)
            {
                return StatusCode(400);
            }
            else
            {
                return Ok();
            }
        }
    }
}
