using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh_Timber_Wolves.Repositories;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpySkillsController : ControllerBase
    {
        private readonly ISpySkillsRepository _spySkillsRepository;
        public SpySkillsController(ISpySkillsRepository spySkillsRepository)
        {
            _spySkillsRepository = spySkillsRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_spySkillsRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var spy = _spySkillsRepository.GetById(id);
            if (spy == null)
            {
                return NotFound();
            }
            return Ok(spy);
        }

        [HttpPost]
        public IActionResult Post(SpySkills spySkills)
        {
            _spySkillsRepository.Add(spySkills);
            return CreatedAtAction("Get", new { id = spySkills.id }, spySkills);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, SpySkills spySkills)
        {
            if (id != spySkills.id)
            {
                return BadRequest();
            }

            _spySkillsRepository.Update(spySkills);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _spySkillsRepository.Delete(id);
            return NoContent();
        }
    }
}
