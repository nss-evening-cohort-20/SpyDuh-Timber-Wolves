using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh_Timber_Wolves.Models;
using SpyDuh_Timber_Wolves.Repositories;

namespace SpyDuh_Timber_Wolves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendRepository _friendRepository;
        public FriendController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_friendRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var friend = _friendRepository.GetByFriendId(id);
            if (friend == null)
            {
                return NotFound();
            }
            return Ok(friend);
        }

        [HttpPost]
        public IActionResult Post(Friend friend)
        {
            _friendRepository.Add(friend);
            return CreatedAtAction("Get", new { id = friend.Id }, friend);
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
            _friendRepository.Delete(id);
            return NoContent();
        }
    }
}
