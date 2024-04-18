using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Models;
using CMS.Models.Dto;
using CMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class QuizController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IQuizRepository _quizRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public QuizController(IMapper mapper, UserManager<User> userManager, IQuizRepository quizRepo, ICourseRepository courseRepo, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _userManager = userManager;
            _quizRepo = quizRepo;
            _courseRepo = courseRepo;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var quizzes = await _quizRepo.GetAll(user.Id);
                return new JsonResult(quizzes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Add(QuizForAddDto quizForAddDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var quiz = _mapper.Map<Quiz>(quizForAddDto);

                quiz.UserId = user.Id;


                _quizRepo.Add(quiz);

                if (await _quizRepo.SaveAll())
                {
                    foreach (var qa in quizForAddDto.QuesAndAns)
                    {
                        qa.QuizId = quiz.Id;
                        _quizRepo.Add(qa);
                    }

                    await _quizRepo.SaveAll();

                    return Ok("Successfully created the quiz");
                }

                return BadRequest("Creating the quiz failed on save");
            }
            catch (Exception e)
            {
                return BadRequest("Creating the quiz failed");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Update(QuizForUpdateDto quizForUpdateDto)
        {
            try
            {

                var quizFromRepo = await _quizRepo.GetOne(quizForUpdateDto.Id);

                if (quizFromRepo == null)
                {
                    return BadRequest("There is no quiz like this");
                }

                _mapper.Map(quizForUpdateDto, quizFromRepo);
                if (await _quizRepo.SaveAll())
                {
                    return Ok("Successfully updated the quiz");
                }

                return BadRequest("Updating the quiz failed");
            }
            catch (Exception e)
            {
                return BadRequest("Updating the quiz failed");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var quizFromRepo = await _quizRepo.GetOne(id);

                if (quizFromRepo == null)
                {
                    return BadRequest("There is no quiz like this");
                }

                _quizRepo.Delete(quizFromRepo);

                if (await _quizRepo.SaveAll())
                {
                    return Ok("Successfully deleted the quiz");
                }

                return BadRequest("Deleting the quiz failed");
            }
            catch (Exception e)
            {
                return BadRequest("Deleting the quiz failed");
            }
        }

        [Authorize(Policy = "RequireStudentRole")]
        [HttpGet]
        public async Task<IActionResult> TakeQuiz()
        {
            try
            {

                var courses = await _courseRepo.GetAll();
                var quizzes = await _quizRepo.GetAll();
                return PartialView("_TakeQuiz", new CourseAndQuizList
                {
                    Courses = courses.ToList(),
                    Quizzes = quizzes.ToList()
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireStudentRole")]
        [HttpGet]
        public async Task<IActionResult> GetQuestions(int quizId)
        {
            try
            {

                var questions = await _quizRepo.GetQuestions(quizId);
                return PartialView("_ExamPaper", questions.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireStudentRole")]
        [HttpPost]
        public async Task<IActionResult> SubmitExam(ExamSubmissionDto examSubDto)
        {
            try
            {

                var qAndA = await _quizRepo.GetQuestions(examSubDto.QuizId);

                if (qAndA == null)
                {
                    return BadRequest("There is no question like this");
                }

                int total = 0;
                int correct = 0;

                foreach (var ques in examSubDto.ExamPaper)
                {
                    total++;
                    if (qAndA.Any(q => q.Id == ques.QuestionId && q.Answer == ques.SelectedOptionNo.ToString()))
                    {
                        correct++;
                    }
                }
                var user = await _userManager.GetUserAsync(User);

                var evaluation = new EvaluationResult
                {
                    UserId = user.Id,
                    QuizId = examSubDto.QuizId,
                    TotalQuestions = total,
                    CorrectAnswer = correct
                };

                _quizRepo.Add(evaluation);

                if (await _quizRepo.SaveAll())
                {
                    return Ok("Successfully added the exam data");
                }

                return BadRequest("Updating the exam data failed");
            }
            catch (Exception e)
            {
                return BadRequest("Updating the exam data failed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                var evaluations = await _quizRepo.GetAllEvaluations();
                return new JsonResult(evaluations);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetXMLQuiz()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string xmlData = Path.Combine(webRootPath, "ExamSheet.xml");

            DataSet ds = new DataSet();//Using dataset to read xml file  
            ds.ReadXml(xmlData);
            var questions = new List<Questions>();
            questions = (from rows in ds.Tables[0].AsEnumerable()
                        select new Questions
                        {
                            Id = Convert.ToInt32(rows[0].ToString()), //Convert row to int  
                            Question = rows[1].ToString(),
                            Option1 = rows[2].ToString(),
                            Option2 = rows[3].ToString(),
                            Option3 = rows[4].ToString(),
                            Option4 = rows[5].ToString(),
                            Answer = rows[6].ToString(),
                        }).ToList();
            return PartialView("XMLExamPaper", questions);
        }
    }
}
