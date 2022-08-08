using DM.BLL.Authorization;
using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly PersonService _personService;
        public AccountController(PersonService personService)
        {
            _personService = personService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetPersons()
        {
            var result = await _personService.GetPersonsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPerson(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id); ;

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("admin")]
        public async Task<ActionResult> PostAdminAccount([FromBody] PersonDTO person)
        {
            try
            {
                var result = await _personService.AddPersonAdminAsync(person.Login, person.Password);
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                return BadRequest("this login is already in use");
            }
        }

        [HttpPost("user")]
        public async Task<ActionResult> PostUserAccount([FromBody] PersonDTO person)
        {
            try
            {
                var result = await _personService.AddPersonUserAsync(person.Login, person.Password);
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                return BadRequest("this login is already in use");
            }
        }

        
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            var account = await _personService.GetPersonByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            await _personService.RemoveAccount(id);
            return NoContent();
        }
    }
}
