using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using roomService.DTO;

namespace roomService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository roomRepo, IMapper mapper)
        {
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery] QueryParams qParams)
        {
            return Ok(_roomRepo.Get(qParams.Sort, qParams.Type));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var room = _roomRepo.Get(id);

            if (room == null)
                return NotFound();

            return Ok(room);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _roomRepo.Add(room);
            if (_roomRepo.SaveAll())
            {
                return Created("", room);
            }
            return BadRequest($"failed to add new room");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoomDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var room = _roomRepo.Get(id);

            if (room == null)
                return NotFound();

            _mapper.Map(dto, room); // (from, to)

            if (_roomRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating room {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = _roomRepo.Get(id);

            if (room == null)
                return NoContent();

            _roomRepo.Delete(room);
            if (_roomRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting room {id} failed on save");
        }
    }
}
