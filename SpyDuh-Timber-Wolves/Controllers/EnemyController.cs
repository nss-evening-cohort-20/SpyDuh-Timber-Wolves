using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh_Timber_Wolves.Models;
using SpyDuh_Timber_Wolves.Repositories;

namespace SpyDuh_Timber_Wolves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnemyController : ControllerBase
    {
        private readonly IEnemyRepository _enemyRepository;
        public EnemyController(IEnemyRepository enemyRepository)
        {
            _enemyRepository = enemyRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_enemyRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var enemy = _enemyRepository.GetByEnemyId(id);
            if (enemy == null)
            {
                return NotFound();
            }
            return Ok(enemy);
        }

        [HttpPost]
        public IActionResult Post(Enemy enemy)
        {
            _enemyRepository.Add(enemy);
            return CreatedAtAction("Get", new { id = enemy.Id }, enemy);
        }

        //[HttpPut("id")]
        //public IActionResult Put(int id, Friend friend)
        //{
        //    if (id != friend.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _friendRepository.Update(friend);
        //    return NoContent();
        //}

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            _enemyRepository.Delete(id);
            return NoContent();
        }
    }
}
