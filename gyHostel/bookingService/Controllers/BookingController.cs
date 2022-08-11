using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using bookingService.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace bookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookingRepo.Get());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var booking = _bookingRepo.Get(id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _bookingRepo.Add(booking);
            if (_bookingRepo.SaveAll())
            {
                return Created("", booking);
            }
            return BadRequest($"failed to add new booking");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var booking = _bookingRepo.Get(id);

            if (booking == null)
                return NotFound();

            _mapper.Map(dto, booking); // (from, to)

            if (_bookingRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating booking {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _bookingRepo.Get(id);

            if (booking == null)
                return NoContent();

            _bookingRepo.Delete(booking);
            if (_bookingRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting booking {id} failed on save");
        }
    }
}
