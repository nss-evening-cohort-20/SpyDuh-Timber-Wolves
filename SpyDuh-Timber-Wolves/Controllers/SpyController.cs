using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh_Timber_Wolves.Repositories;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpyController : ControllerBase
    {
        private readonly ISpyRepository _spyRepository;
        public SpyController(ISpyRepository spyRepository)
        {
            _spyRepository = spyRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_spyRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            var spy = _spyRepository.GetById(id);
            if (spy == null) 
            {
                return NotFound();
            }
            return Ok(spy);
        }

        [HttpPost]
        public IActionResult Post(Spy spy)
        {
            _spyRepository.Add(spy);
            return CreatedAtAction("Get", new { id = spy.id }, spy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Spy spy)
        {
            if (id != spy.id)
            {
                return BadRequest();
            }

            _spyRepository.Update(spy);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _spyRepository.Delete(id);
            return NoContent();
        }
    }
}
