using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TakeItToTheCloud.Models.Dto;
using TakeItToTheCloud.Services;

namespace TakeItToTheCloud.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index(bool all = false)
        {
            int userid = 0;
            if (!all)
            {
                int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out userid);
            }

            var uploadedFiles = await _dashboardService.GetUploadedFiles(userid);
            return View(uploadedFiles);
        }

        [HttpGet]
        public IActionResult AddUpdateUploadedFile()
        {
            return PartialView("_AddUpdateUploadFile");
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateUploadedFile(FileUploadDto files)
        {

            try
            {
                int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userid);
                var ret = await _dashboardService.AddUploadedFile(userid, files);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFile(int id)
        {

            try
            {
                var ret = await _dashboardService.DeleteFile(id);

                return Ok(new { success = true, message = "File deleted Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = true, message = "We are facing some problem. Try again!" });
            }
        }
    }
}
