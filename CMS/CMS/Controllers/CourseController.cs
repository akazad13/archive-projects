using System;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Models;
using CMS.Models.Dto;
using CMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ICourseRepository _courseRepo;

        public CourseController(IMapper mapper, UserManager<User> userManager, ICourseRepository courseRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _courseRepo = courseRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var courses = await _courseRepo.GetAll(user.Id);
                return new JsonResult(courses);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Add(CourseForAddDto courseForAddDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var course = _mapper.Map<Course>(courseForAddDto);

                course.UserId = user.Id;

                _courseRepo.Add(course);

                if (await _courseRepo.SaveAll())
                {
                    return Ok("Successfully created the course");
                }

                return BadRequest("Creating the course failed on save");
            }
            catch (Exception e)
            {
                return BadRequest("Creating the course failed");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Update(CourseForUpdateDto courseForUpdateDto)
        {
            try
            {

                var courseFromRepo = await _courseRepo.GetOne(courseForUpdateDto.Id);

                if(courseFromRepo == null)
                {
                    return BadRequest("There is no course like this");
                }

                _mapper.Map(courseForUpdateDto, courseFromRepo);
                if (await _courseRepo.SaveAll())
                {
                    return Ok("Successfully updated the course");
                }

                return BadRequest("Updating the course failed");
            }
            catch (Exception e)
            {
                return BadRequest("Updating the course failed");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var courseFromRepo = await _courseRepo.GetOne(id);

                if (courseFromRepo == null)
                {
                    return BadRequest("There is no course like this");
                }

                _courseRepo.Delete(courseFromRepo);
                
                if (await _courseRepo.SaveAll())
                {
                    return Ok("Successfully deleted the course");
                }

                return BadRequest("Deleting the course failed");
            }
            catch (Exception e)
            {
                return BadRequest("Deleting the course failed");
            }
        }
    }
}
