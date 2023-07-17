using edu_services.Domain;
using edu_services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace edu_services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        Classroom<Teacher, Student> classroom1 = new Classroom<Teacher, Student>();
        protected IMemoryCache _cache;
        public ApiController(ILogger<ApiController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache= cache;
            if (!_cache.TryGetValue("Class1", out var classRoom))
            {
                _cache.Set("Class1", classroom1);
            }
        }



        /// <summary>
        /// Add a teacher to the classroom
        /// </summary>
        /// <param name="teacher">Teacher object</param>
        /// <returns></returns>
        [HttpPost("Teacher")]
        public IActionResult AddTeacher(Teacher teacher)
        {
            try
            {
                if (_cache.TryGetValue("Class1", out var classRoom))
                {
                    var Currentclassroom = (Classroom<Teacher, Student>)classRoom;
                    Currentclassroom.AddTeacher(teacher);
                    _cache.Set("Class1", Currentclassroom);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            _logger.LogInformation("Teacher {0} added to the Classroom", teacher.TeacherName);
            return Ok();
        }

        /// <summary>
        /// Add a student to Classroom
        /// </summary>
        /// <param name="student">Student object</param>
        /// <returns></returns>
        [HttpPost("Student")]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                if (_cache.TryGetValue("Class1", out var classRoom))
                {
                    var Currentclassroom = (Classroom<Teacher, Student>)classRoom;
                    Currentclassroom.AddStudent(student);
                    _cache.Set("Class1", Currentclassroom);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            _logger.LogInformation("Student {0} added to the Classroom", student.Name);
            return Ok();
            
        }

        /// <summary>
        /// Add a student to Classroom
        /// </summary>
        /// <param name="students">List of Student object</param>
        /// <returns></returns>
        [HttpPost("Students")]
        public IActionResult AddStudents(List<Student> students)
        {
            try
            {
                if (_cache.TryGetValue("Class1", out var classRoom))
                {
                    var Currentclassroom = (Classroom<Teacher, Student>)classRoom;
                    foreach (var student in students)
                    {
                        Currentclassroom.AddStudent(student);
                        _logger.LogInformation("Student {0} added to the Classroom", student.Name);
                    }
                    _cache.Set("Class1", Currentclassroom);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            return Ok();

        }

        /// <summary>
        /// Returns Class room roster
        /// </summary>
        /// <returns>Classroom object</returns>
        [HttpGet("ClassroomRoster")]
        public ActionResult<Classroom<Teacher, Student>> GetClassroomRoster()
        {
            
            (Teacher, List<Student>) roster = new();
            try
            {
                if (_cache.TryGetValue("Class1", out var classRoom))
                {
                    var Currentclassroom = (Classroom<Teacher, Student>)classRoom;
                    roster = Currentclassroom.GetRoster();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }

            //changing the tuple to classroom object
            Classroom<Teacher, Student> classRoster = new Classroom<Teacher, Student>(roster.Item2,roster.Item1);
            
            //this will return the roster in Json format by default
            return Ok(classRoster);

        }

    }
}
