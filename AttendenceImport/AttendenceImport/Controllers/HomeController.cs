using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AttendenceImport.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ExcelDataReader;
using AttendenceImport.Repository;

namespace AttendenceImport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IAttendenceRepository _attendenceRepo;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, IAttendenceRepository attendenceRepo)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _attendenceRepo = attendenceRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormCollection reportfile)
        {
            if (reportfile.Files.Count == 0)
            {
                return Redirect("/?s=0&errmsg=Please import a file.");
            }

            try
            {



                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                // Delete Files from Directory
                System.IO.DirectoryInfo di = new DirectoryInfo(newPath);
                foreach (FileInfo filesDelete in di.GetFiles())
                {
                    filesDelete.Delete();
                }

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                var fiName = Guid.NewGuid().ToString() + Path.GetExtension(reportfile.Files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(newPath, fiName), FileMode.Create))
                {
                    reportfile.Files[0].CopyTo(fileStream);
                }
                string rootFolder = _hostingEnvironment.WebRootPath;
                string fileName = @"Upload/" + fiName;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(Path.Combine(rootFolder, fileName), FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        List<ExcelData> excelData = new List<ExcelData>();

                        bool ignoreFirstRow = true;

                        while (reader.Read()) //Each row of the file
                        {
                            if (ignoreFirstRow)
                            {
                                ignoreFirstRow = false;
                                continue;
                            }

                            var maxCol = reader.FieldCount;

                            int.TryParse(GetValue(reader, 1), out int studentId);

                            excelData.Add(new ExcelData
                            {
                                StudentName = GetValue(reader, 0),
                                StudentID = studentId,
                                Programme = GetValue(reader, 2),
                                ClassCode = GetValue(reader, 3),
                                StudentStatus = GetValue(reader, 4),
                                AttendanceStatus = GetValue(reader, 5),

                                L1 = GetValue(reader, 6)?[0],
                                Attendance1 = GetValue(reader, 7),

                                L2 = GetValue(reader, 8)?[0],
                                Attendance2 = GetValue(reader, 9),

                                L3 = GetValue(reader, 10)?[0],
                                Attendance3 = GetValue(reader, 11),

                                L4 = GetValue(reader, 12)?[0],
                                Attendance4 = GetValue(reader, 13),

                                L5 = GetValue(reader, 14)?[0],
                                Attendance5 = GetValue(reader, 15),

                                L6 = GetValue(reader, 16)?[0],
                                Attendance6 = GetValue(reader, 17),

                                L7 = GetValue(reader, 18)?[0],
                                Attendance7 = GetValue(reader, 19),

                                L8 = GetValue(reader, 20)?[0],
                                Attendance8 = GetValue(reader, 21),

                                L9 = GetValue(reader, 22)?[0],
                                Attendance9 = GetValue(reader, 23),

                                L10 = GetValue(reader, 24)?[0],
                                Attendance10 = GetValue(reader, 25),

                                L11 = GetValue(reader, 26)?[0],
                                Attendance11 = GetValue(reader, 27),

                                L12 = GetValue(reader, 28)?[0],
                                Attendance12 = GetValue(reader, 29),

                                L13 = GetValue(reader, 30)?[0],
                                Attendance13 = GetValue(reader, 31),

                                L14 = GetValue(reader, 32)?[0],
                                Attendance14 = GetValue(reader, 33),

                                L15 = GetValue(reader, 34)?[0],
                                Attendance15 = GetValue(reader, 35),

                                L16 = GetValue(reader, 36)?[0],
                                Attendance16 = GetValue(reader, 37),

                                L17 = GetValue(reader, 38)?[0],
                                Attendance17 = GetValue(reader, 39),

                                L18 = GetValue(reader, 40)?[0],
                                Attendance18 = GetValue(reader, 41),

                                L19 = GetValue(reader, 42)?[0],
                                Attendance19 = GetValue(reader, 43),

                                L20 = GetValue(reader, 44)?[0],
                                Attendance20 = GetValue(reader, 45),

                                L21 = GetValue(reader, 46)?[0],
                                Attendance21 = GetValue(reader, 47),

                                L22 = GetValue(reader, 48)?[0],
                                Attendance22 = GetValue(reader, 49),

                                L23 = GetValue(reader, 50)?[0],
                                Attendance23 = GetValue(reader, 51),

                                L24 = GetValue(reader, 52)?[0],
                                Attendance24 = GetValue(reader, 53),

                                L25 = GetValue(reader, 54)?[0],
                                Attendance25 = GetValue(reader, 55),

                                L26 = GetValue(reader, 56)?[0],
                                Attendance26 = GetValue(reader, 57),

                                L27 = GetValue(reader, 58)?[0],
                                Attendance27 = GetValue(reader, 59),

                                L28 = GetValue(reader, 60)?[0],
                                Attendance28 = GetValue(reader, 61),

                                L29 = GetValue(reader, 62)?[0],
                                Attendance29 = GetValue(reader, 63),

                                L30 = GetValue(reader, 64)?[0],
                                Attendance30 = GetValue(reader, 65),

                                L31 = GetValue(reader, 66)?[0],
                                Attendance31 = GetValue(reader, 67),

                                L32 = GetValue(reader, 68)?[0],
                                Attendance32 = GetValue(reader, 69),

                                L33 = GetValue(reader, 70)?[0],
                                Attendance33 = GetValue(reader, 71),

                                L34 = GetValue(reader, 72)?[0],
                                Attendance34 = GetValue(reader, 73),

                                L35 = GetValue(reader, 74)?[0],
                                Attendance35 = GetValue(reader, 75),

                                L36 = GetValue(reader, 76)?[0],
                                Attendance36 = GetValue(reader, 77),

                                L37 = GetValue(reader, 78)?[0],
                                Attendance37 = GetValue(reader, 79),

                                L38 = GetValue(reader, 80)?[0],
                                Attendance38 = GetValue(reader, 81),

                                L39 = GetValue(reader, 82)?[0],
                                Attendance39 = GetValue(reader, 83),

                                L40 = GetValue(reader, 84)?[0],
                                Attendance40 = GetValue(reader, 85),

                                L41 = GetValue(reader, 86)?[0],
                                Attendance41 = GetValue(reader, 87),

                                L42 = GetValue(reader, 88)?[0],
                                Attendance42 = GetValue(reader, 89),

                                L43 = GetValue(reader, 90)?[0],
                                Attendance43 = GetValue(reader, 91),

                                L44 = GetValue(reader, 92)?[0],
                                Attendance44 = GetValue(reader, 93),

                                L45 = GetValue(reader, 94)?[0],
                                Attendance45 = GetValue(reader, 95),

                                L46 = GetValue(reader, 96)?[0],
                                Attendance46 = GetValue(reader, 97),

                                L47 = GetValue(reader, 98)?[0],
                                Attendance47 = GetValue(reader, 99),

                                L48 = GetValue(reader, 100)?[0],
                                Attendance48 = GetValue(reader, 101),

                                L49 = GetValue(reader, 102)?[0],
                                Attendance49 = GetValue(reader, 103),

                                L50 = GetValue(reader, 104)?[0],
                                Attendance50 = GetValue(reader, 105),

                                L51 = GetValue(reader, 106)?[0],
                                Attendance51 = GetValue(reader, 107),

                                L52 = GetValue(reader, 108)?[0],
                                Attendance52 = GetValue(reader, 109),

                                L53 = GetValue(reader, 110)?[0],
                                Attendance53 = GetValue(reader, 111),

                                L54 = GetValue(reader, 112)?[0],
                                Attendance54 = GetValue(reader, 113),

                                L55 = GetValue(reader, 114)?[0],
                                Attendance55 = GetValue(reader, 115),

                                L56 = GetValue(reader, 116)?[0],
                                Attendance56 = GetValue(reader, 117),
                            });
                        }

                        try
                        {
                            _attendenceRepo.AddRange(excelData);

                            if (await _attendenceRepo.SaveAll())
                            {
                                return Redirect("/?s=1");
                            }
                            else
                            {
                                return Redirect("/?s=0&errmsg=Failed to import the data");
                            }
                        }
                        catch (Exception ex)
                        {
                            return Redirect("/?s=0&errmsg=" + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Redirect("/?s=0&errmsg=" + ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetValue(IExcelDataReader reader, int col)
        {
            return reader.FieldCount > col ? reader.GetValue(col)?.ToString() : null;
        }
    }
}
