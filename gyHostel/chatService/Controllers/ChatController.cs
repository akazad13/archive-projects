using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using chatService.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepo;
        private readonly IMapper _mapper;

        public ChatController(IChatRepository chatRepo, IMapper mapper)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_chatRepo.Get());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var chat = _chatRepo.Get(id);

            if (chat == null)
                return NotFound();

            return Ok(chat);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Chat chat)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _chatRepo.Add(chat);
            if (_chatRepo.SaveAll())
            {
                return Created("", chat);
            }
            return BadRequest($"failed to add new chat");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ChatDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var chat = _chatRepo.Get(id);

            if (chat == null)
                return NotFound();

            _mapper.Map(dto, chat); // (from, to)

            if (_chatRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating chat {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var chat = _chatRepo.Get(id);

            if (chat == null)
                return NoContent();

            _chatRepo.Delete(chat);
            if (_chatRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting chat {id} failed on save");
        }
    }
}
