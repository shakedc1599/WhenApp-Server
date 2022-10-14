using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whenAppModel.Models;
using whenAppModel.Services;

namespace WhenUp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {

        private readonly IContactsService contactService;
        private readonly IUsersService userService;

        public ContactsController(IContactsService ContactService, IUsersService UserService)
        {
            contactService = ContactService;
            userService = UserService;
        }

        public class ContactsPayload
        {
            public string? id { get; set; }
            public string? name { get; set; }
            public string? server { get; set; }
        }

        [HttpGet]
        [NonAction]
        public async Task<User?> GetCurrentUser()
        {
            var user = User.FindFirst("UserId")?.Value;
            if (user == null) return null;
            //Request.Headers
            return await userService.Get(user);
        }


        // GET: Contacts - action number 1
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> GetAllContacts()
        {
            User? currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                var r = await contactService.GetAllContacts(currentUser);
                return Ok(r);
            }
            return NotFound("token is incorrect");
        }

        //POST: Contacts - action number 2
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> AddContact([FromBody] ContactsPayload payload)
        {
            User? currentUser = await GetCurrentUser();
            if (currentUser == null)
            {
                return NotFound();
            }
            if (await contactService.GetContact(currentUser.Username, payload.id) != null)
            {
                return BadRequest(new { message = "the contact is already exists" });
            }

            Contact contact = await contactService.AddContact(currentUser.Username, payload.id, payload.name, payload.server);
            return Ok(contact);
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        //GET: Contacts/id - action number 3
        [Route("{id}")]
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> GetDetails(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User currentUser = await GetCurrentUser();
            Contact contact = await contactService.GetContact(currentUser.Username, id);
            if (contact == null)
            {
                return BadRequest(new { message = "he is not your contact" });
            }
            return Ok(contact);
        }


        //PUT: Contacts/id - action number 4
        [HttpPut]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<IActionResult> UpdateContact(string id, [FromBody] ContactsPayload payload)
        {
            User currentUser = await GetCurrentUser();
            Contact contact = await contactService.GetContact(currentUser.Username, id);

            if (contact == null)
                return NotFound();

            await contactService.UpdateContact(currentUser.Username, id, payload.name, payload.server);

            return NoContent();
        }

        //POST: Contacts/id - action number 5
        [HttpDelete]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            User currentUser = await GetCurrentUser();

            if (!await contactService.DeleteContact(currentUser.Username, id))
            {
                return BadRequest(new { message = "the contcat is not exsist" });
            }
            return NoContent();
        }
    }
}
