using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using recordService.DTO;

namespace recordService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _recoredRepo;
        private readonly IMapper _mapper;

        public RecordController(IRecordRepository recoredRepo, IMapper mapper)
        {
            _recoredRepo = recoredRepo;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_recoredRepo.Get());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var record = _recoredRepo.Get(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Record record)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _recoredRepo.Add(record);
            if (_recoredRepo.SaveAll())
            {
                return Created("", record);
            }
            return BadRequest($"failed to add new recoard");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RecordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var record = _recoredRepo.Get(id);

            if (record == null)
                return NotFound();

            _mapper.Map(dto, record); // (from, to)

            if (_recoredRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating record {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = _recoredRepo.Get(id);

            if (record == null)
                return NoContent();

            _recoredRepo.Delete(record);
            if (_recoredRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting record {id} failed on save");
        }
    }
}
