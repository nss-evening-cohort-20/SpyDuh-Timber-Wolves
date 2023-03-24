using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh_Timber_Wolves.Repositories;
using SpyDuh_Timber_Wolves.Models;


namespace SpyDuh_Timber_Wolves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpyServicesController : ControllerBase
    {
        private readonly ISpyServicesRepository _spyServicesRepository;
        public SpyServicesController(ISpyServicesRepository spyServicesRepository)
        {
            _spyServicesRepository = spyServicesRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_spyServicesRepository.GetAll());
        }

        [HttpGet("spy/{spyId}")]
        public IActionResult GetSpiesServices(int spyId)
        {
            var spy = _spyServicesRepository.GetBySpyId(spyId);
            if (spy == null)
            {
                return NotFound();
            }
            return Ok(spy);
        }

        [HttpGet("{serviceId}")]
        public IActionResult GetService(int serviceId)
        {
            var service = _spyServicesRepository.GetByServiceId(serviceId);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }
        [HttpGet("service/{serviceName}")]
        public IActionResult GetServiceByName(string serviceName)
        {
            var service = _spyServicesRepository.GetByServiceName(serviceName);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }


        [HttpPost]
        public IActionResult Post(SpyServices spyServices)
        {
            _spyServicesRepository.Add(spyServices);
            return CreatedAtAction("Get", new { id = spyServices.id }, spyServices);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, SpyServices spyServices)
        {
            if (id != spyServices.id)
            {
                return BadRequest();
            }

            _spyServicesRepository.Update(spyServices);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _spyServicesRepository.Delete(id);
            return NoContent();
        }
    }
}
