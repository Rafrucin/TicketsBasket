using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Services;

namespace TicketsBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfilesService _userProfiles;

        public UserProfilesController(IUserProfilesService userProfiles)
        {
            _userProfiles = userProfiles;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userProfiles.GetProfileByUSerIdAsync();

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound();
        }

    }
}
