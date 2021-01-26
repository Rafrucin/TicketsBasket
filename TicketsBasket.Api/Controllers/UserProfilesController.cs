using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Services;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

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

        [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfileDetail>))]
        [ProducesResponseType(404)]
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

        [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfileDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfileDetail>))]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreateProfileRequest model)
        {
            var result = await _userProfiles.CreateProfileAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfileDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfileDetail>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm]IFormFile file)
        {
            var result = await _userProfiles.UpdateProfilePicture(file);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
