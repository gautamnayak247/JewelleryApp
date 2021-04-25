namespace Jewellery.Api.Controllers.v1
{
    using Jewellery.Api.Filters;
    using Jewellery.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/v1/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;
        public UsersController(IUserService _service)
            => service = _service ?? throw new ArgumentNullException(nameof(_service));

        /// <summary>
        /// Getting loggedin user information
        /// </summary>
        /// <returns></returns>
        [Authentication]
        [HttpHead, HttpGet(Name = "v1/GetUserById")]
        public async Task<IActionResult> GetUser()
        {
            var user = await service.GetUserAsync().ConfigureAwait(false);
            return Ok(user);
        }
    }
}
