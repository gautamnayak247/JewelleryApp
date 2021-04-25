namespace Jewellery.Api.Controllers.v1
{
    using Jewellery.Api.Filters;
    using Jewellery.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/v1/price")]
    public class PriceController : ControllerBase
    {
        private readonly ICalculationService service;

        public PriceController(ICalculationService _service)
        {
            service = _service ?? throw new ArgumentNullException(nameof(_service));
        }
        /// <summary>
        /// Calculation of price based on gold price and weight
        /// </summary>
        /// <param name="goldPrice"></param>
        /// <param name="weight"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        [Authentication]
        [HttpHead, HttpGet("{goldPrice}/{weight}", Name = "v1/calculateprice")]
        public IActionResult Calculateprice(double goldPrice, double weight, [FromQuery] double discount = 2)
        {
            return Ok(service.CalculatePrice(goldPrice, weight, discount));
        }
    }
}
