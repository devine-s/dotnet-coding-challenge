using CMS.challenge.data.Cache;
using CMS.challenge.data.Entities;
using CMS.challenge.common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

            List<Error> errorList = ValidationClass.ValidInput(user, _simpleObjectCache, false);
            if (errorList.Count != 0)
            {
                return BadRequest(new { errorList });
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
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var record = await _simpleObjectCache.GetAsync(id);

            if (record == null)
            {
                return NotFound(record);
            }
            
            return Ok(record);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var record = await _simpleObjectCache.GetAsync(id);

            if (record == null)
            {
                return NotFound(record);
            }

            await _simpleObjectCache.DeleteAsync(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutUserAsync([FromBody] User user)
        {
            List<Error> errorList = ValidationClass.ValidInput(user, _simpleObjectCache, true);
            if (errorList.Count != 0)
            {
                return BadRequest(new { errorList });
            }

            await _simpleObjectCache.UpdateAsync(user.Id, user);

            return Ok();
        }
    }
}
