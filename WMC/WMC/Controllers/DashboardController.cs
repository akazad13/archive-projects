using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WMC.Models.Dto;
using WMC.Services;

namespace WMC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            int userid = 0;
            if(User.IsInRole("Customer"))
            {
                int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out userid);
                ViewBag.role = "customer";
            }

            if(User.IsInRole("Manager"))
            {
                ViewBag.isManager = true;
            }

            var consultation = await _dashboardService.GetConsultations(userid);
            return View(consultation);
        }

        [HttpGet]
        public IActionResult AddConsultation()
        {
            return PartialView("_AddConsultation");
        }

        [HttpPost]
        public async Task<IActionResult> AddConsultation(AddConsultationDto consultation)
        {
            
            try
            {
                int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userid);
                var ret = await _dashboardService.AddConsultation(userid, consultation);
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        [Authorize(Policy = "RequireManagerOrStaffRole")]
        public async Task<IActionResult> UpdateConsultation(int consultationId)
        {
            var consultation = await _dashboardService.GetConsultation(consultationId);
            return PartialView("_UpdateConsultation", consultation);
        }

        [HttpPost]
        [Authorize(Policy = "RequireManagerOrStaffRole")]
        public async Task<IActionResult> UpdateConsultation(UpdateConsultationDto consultation)
        {
            try
            {
                var ret = await _dashboardService.UpdateConsultation(consultation);
                if (ret == true)
                {
                    return Ok(new { success = true, message = "Successfully updated consultation request." });
                }
                else
                {
                    return Ok(new { success = false, message = "Failed to update consultation request." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUploadedFileLink(int consultationId)
        {
            var url = await _dashboardService.GetUploadedFileLink(consultationId);
            return Ok(new { succss = true, url = url });
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var messages = await _dashboardService.GetNotifications();
            return PartialView("_Notifications", messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            int userid = 0;
            if (User.IsInRole("Customer"))
            {
                int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out userid);
                ViewBag.role = "customer";
            }
            var consultation = await _dashboardService.GetFeedbacks(userid);
            return PartialView("_Feedbacks", consultation);
        }

        [HttpGet]
        public IActionResult AddFeedback()
        {
            return PartialView("_AddFeedback");
        }

        [HttpPost]
        public async Task<IActionResult>AddFeedback(AddFeedbackDto addFeedback)
        {
            try
            {
                int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userid);
                var ret = await _dashboardService.AddFeedback(userid, addFeedback);
                if (ret == true)
                {
                    return Ok(new { success = true, message = "Successfully posted feedback." });
                }
                else
                {
                    return Ok(new { success = false, message = "Failed to post feedback." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> GetStaffs()
        {
            var staffs = await _dashboardService.GetStaffs();
            return PartialView("_StaffList", staffs);
        }

        [HttpGet]
        [Authorize(Policy = "RequireManagerRole")]
        public IActionResult AddStaff()
        {
            return PartialView("_AddNewStaff");
        }

        [HttpPost]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> AddStaff(AddStaffDto staff)
        {
            try
            {
                var ret = await _dashboardService.AddStaff(staff);
                if (ret == true)
                {
                    return Ok(new { success = true, message = "Successfully added staff." });
                }
                else
                {
                    return Ok(new { success = false, message = "Failed to add Staff." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
