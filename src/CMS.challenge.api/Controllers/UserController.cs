using CMS.challenge.data.Cache;
using CMS.challenge.data.Entities;
using CMS.challenge.common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CMS.challenge.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : Controller
    {
        private readonly ISimpleObjectCache<Guid, User> _simpleObjectCache;

        public UserController(ISimpleObjectCache<Guid, User> SimpleObjectCache)
        {
            _simpleObjectCache = SimpleObjectCache;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            if (user.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                user.Id = Guid.NewGuid();
            }

            if (ValidationClass.UserExists(user.Id, _simpleObjectCache))
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"User already exists\"}");
                return BadRequest(response);
            }

            if (ValidationClass.EmailExists(user.Email, _simpleObjectCache))
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"Email address already used\"}");
                return BadRequest(response);
            }

            if (!ValidationClass.IsValidFirstName(user.FirstName))
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"First name is greater than 128 characters\"}");
                return BadRequest(response);
            }

            if (!ValidationClass.IsValidLastName(user.LastName))
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"Last name is greater than 128 characters\"}");
                return BadRequest(response);
            }

            if (!ValidationClass.IsValidEmail(user.Email))
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"Email isn't valid\"}");
                return BadRequest(response);
            }

            if (!ValidationClass.IsValidDateOfBirth(user.DateOfBirth))
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"Date of birth isn't valid\"}");
                return BadRequest(response);
            }

            await _simpleObjectCache.AddAsync(user.Id, user);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _simpleObjectCache.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult PutUser()
        {
            return Ok();
        }
    }
}
