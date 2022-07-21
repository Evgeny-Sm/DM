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

        [HttpPost("token")]
        public IActionResult Token([FromBody] PersonDTO person)
        {
            if (person == null)
            {
                return BadRequest(new { errorText = "Invalid username or password" });
            }
            var identity = _personService.GetIdentity(person.Login, person.Password);

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password" });
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responce = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = _personService.GetRole(person.Login).Result
            };

            return new JsonResult(responce);
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
