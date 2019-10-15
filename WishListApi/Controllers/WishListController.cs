using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WishListApi.Models;

namespace WishListApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IWishListRepository _repository;
        private readonly ILogger _logger;
        public WishListController(IWishListRepository repository,ILoggerFactory logger)
        {
            _repository = repository;
            _logger = logger.CreateLogger<WishListController>();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WishList),(int)HttpStatusCode.OK)]
        public async Task<IActionResult>Get(string id)
        {
            var wishList = await _repository.GetWishListAsync(id);
            return Ok(wishList);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WishList),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]WishList value)
        {
            var wishList = await _repository.UpdateWishListAsync(value);
            return Ok(wishList);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _logger.LogInformation("Delete WisList method in WishList controller");
            _repository.DeleteWishListAsync(id);
        }
    }
}