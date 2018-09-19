using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SQLClient_Web.Helpers;
using SQLClient_Web.Models;
using SQLClient_Web.Repositories;

namespace SQLClient_Web.Controllers
{
    [Route("address")]
    [Produces("application/json")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private IRepository<Address> _repository;
        private IAuthenticator _authenticator;
        public AddressController(ILoggerFactory loggerFactory, IRepository<Address> repository, IAuthenticator authenticator)
        {
            _logger = loggerFactory.CreateLogger<AddressController>();
            _repository = repository;
            _authenticator = authenticator;
        }

        [HttpGet]
        public IActionResult Get([FromHeader] string Authorization)
        {
            if (!_authenticator.IsAuthenticated(Authorization))
            {
                _logger.LogInformation("Unauthenticated Access");
                return Unauthorized();
            }
            _logger.LogInformation("GetAddress");

            var result = _repository.ReadAll();
            return result.Any() ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromHeader] string Authorization)
        {
            if (!_authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = _repository.Read(id);
            return result != null ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Address address, [FromHeader] string Authorization)
        {
            if (!_authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = _repository.Create(address);
            return result > 0 ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Address address, [FromHeader] string Authorization)
        {
            if (!_authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            address.Id = id;
            var result = _repository.Update(address);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string Authorization)
        {
            if (!_authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = _repository.Delete(id);
            return Ok(result);
        }
    }
}
