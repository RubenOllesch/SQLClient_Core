using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLClient_Web.Helpers;
using SQLClient_Web.Models;
using SQLClient_Web.Repositories;

namespace SQLClient_Web.Controllers
{
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IRepository<Address> repository;
        private IAuthenticator authenticator;
        public AddressController(IRepository<Address> repository,IAuthenticator authenticator)
        {
            this.repository = repository;
            this.authenticator = authenticator;
        }

        [HttpGet]
        public IActionResult Get([FromHeader] string Authorization)
        {
            if (!authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = repository.ReadAll();
            return result.Any() ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromHeader] string Authorization)
        {
            if (!authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = repository.Read(id);
            return result != null ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Address address, [FromHeader] string Authorization)
        {
            if (!authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = repository.Create(address);
            return result > 0 ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Address address, [FromHeader] string Authorization)
        {
            if (!authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            address.Id = id;
            var result = repository.Update(address);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string Authorization)
        {
            if (!authenticator.IsAuthenticated(Authorization))
            {
                return Unauthorized();
            }
            var result = repository.Delete(id);
            return Ok(result);
        }
    }
}
