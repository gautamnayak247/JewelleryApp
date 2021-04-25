namespace Jewellery.Api.Controllers.v1
{
    using Jewellery.Application.Interfaces;
    using Jewellery.Application.Models.v1;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("api/v1/login")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ILogInService service;

        public LogInController(ILogInService service)
            => this.service = service ?? throw new ArgumentNullException(nameof(service));

        /// <summary>
        /// Login to the app
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogInModel model)
        {
            var authToken = await service.GetTokenAsync(model).ConfigureAwait(false);
            return authToken == default ? Unauthorized() : (IActionResult)Ok(authToken);
        }
    }
}
