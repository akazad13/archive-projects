using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceRepository _financeRepo;
        private readonly IMapper _mapper;

        public FinanceController(IFinanceRepository financeRepo, IMapper mapper)
        {
            _financeRepo = financeRepo;
            _mapper = mapper;
        }
        // GET: api/Finances
        [HttpGet]
        public IActionResult GetFinance()
        {
            return Ok(_financeRepo.Get());
        }

        // GET: api/Finances/5
        [HttpGet("{id}")]
        public  IActionResult GetFinance(int id)
        {
            var finance = _financeRepo.Get(id);

            if (finance == null)
            {
                return NotFound();
            }

            return Ok(finance);
        }

        // PUT: api/Finances/5
        [HttpPut("{id}")]
        public IActionResult PutFinance(int id, Finance finance)
        {
            if (id != finance.Id)
            {
                return BadRequest();
            }

            var financeFromDB = _financeRepo.Get(id);

            if (financeFromDB == null)
                return NotFound();

            _mapper.Map(finance, financeFromDB); // (from, to)

            if (_financeRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating finance {id} failed on save");
        }

        [HttpPost]
        public  IActionResult PostFinance(Finance finance)
        {

            _financeRepo.Add(finance);
            if (_financeRepo.SaveAll())
            {
                return CreatedAtAction("GetFinance", new { id = finance.Id }, finance);
            }
            return BadRequest($"failed to add new finance");

            
        }

        // DELETE: api/Finances/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFinance(int id)
        {
            var finance = _financeRepo.Get(id);
            if (finance == null)
            {
                return NotFound();
            }

            _financeRepo.Delete(finance);
            if (_financeRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting room {id} failed on save");
        }
    }
}
