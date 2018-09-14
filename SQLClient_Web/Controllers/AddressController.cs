using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLClient_Web.Models;
using SQLClient_Web.Repositories;

namespace SQLClient_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IRepository<Address> repository;
        public AddressController(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = repository.ReadAll();
            return result.Any() ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = repository.Read(id);
            return result != null ? Ok(result) : (IActionResult)StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string props)
        {
            Address address = new Address();
            MapPropsToAddress(address, props.Split(" "));
            var result = repository.Create(address);
            return result > 0 ? (IActionResult)StatusCode(StatusCodes.Status201Created) : Ok(false);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string props)
        {
            Address address = new Address();
            MapPropsToAddress(address, props.Split(" "));
            var result = repository.Update(address);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = repository.Delete(id);
            return Ok(result);
        }

        private void MapPropsToAddress(Address address, string[] args)
        {
            if (address == null)
            {
                return;
            }
            foreach (string argument in args)
            {
                string propName = argument.Split("=")[0];
                string propValue = argument.Split("=")[1];
                switch (propName)
                {
                    case "Country":
                        address.Country = propValue;
                        break;
                    case "City":
                        address.City = propValue;
                        break;
                    case "ZIP":
                        address.ZIP = propValue;
                        break;
                    case "Street":
                        address.Street = propValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
