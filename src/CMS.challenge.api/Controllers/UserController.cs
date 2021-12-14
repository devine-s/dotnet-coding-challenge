using CMS.challenge.data.Cache;
using CMS.challenge.data.Entities;
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
            var existingRecord = _simpleObjectCache.GetAsync(user.Id);
            if (existingRecord.Result != null)
            {
                JObject response = JObject.Parse("{ \"ErrorMessage\": \"User already exists\"}");
                return BadRequest(response);
            }

            if (user.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                user.Id = Guid.NewGuid();
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
