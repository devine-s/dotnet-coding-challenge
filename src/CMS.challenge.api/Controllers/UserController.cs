using CMS.challenge.data.Cache;
using CMS.challenge.data.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateUserAsync()
        {
            var user = new User
            {
                DateOfBirth = DateTime.Now.AddYears(-10),
                Email = "",
                FirstName = "",
                LastName = "",
                Id = Guid.NewGuid()
            };

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
