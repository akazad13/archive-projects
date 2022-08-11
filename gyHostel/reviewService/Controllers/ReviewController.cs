using System.Collections.Generic;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reviewService.DTO;

namespace reviewService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_reviewRepo.Get());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var review = _reviewRepo.Get(id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _reviewRepo.Add(review);
            if (_reviewRepo.SaveAll())
            {
                return Created("", review);
            }
            return BadRequest($"failed to add new review");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var review = _reviewRepo.Get(id);

            if (review == null)
                return NotFound();

            _mapper.Map(dto, review); // (from, to)

            if (_reviewRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating review {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _reviewRepo.Get(id);

            if (review == null)
                return NoContent();

            _reviewRepo.Delete(review);
            if (_reviewRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting review {id} failed on save");
        }
    }
}
