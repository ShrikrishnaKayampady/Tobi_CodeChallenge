﻿using edu_services.Domain;
using edu_services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace edu_services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        Classroom<Teacher, Student> classRoom1 = new Classroom<Teacher, Student>();
        protected IMemoryCache _cache;
        public ApiController(ILogger<ApiController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
            if (!_cache.TryGetValue("Class1", out var classRoom))
            {
                _cache.Set("Class1", classRoom1);
            }
            else
            {
                classRoom1 = (Classroom<Teacher, Student>)classRoom;
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
                classRoom1.AddTeacher(teacher);
                _cache.Set("Class1", classRoom1);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            string message = $"Teacher '{teacher.TeacherName}' added to the Classroom";
            _logger.LogInformation(message);
            return Ok(message);
        }


        /// <summary>
        /// Add students to Classroom
        /// </summary>
        /// <param name="students">List of Student object</param>
        /// <returns></returns>
        [HttpPost("Students")]
        public IActionResult AddStudents(List<Student> students)
        {
            try
            {
                foreach (var student in students)
                {
                    classRoom1.AddStudent(student);
                    _logger.LogInformation("Student {0} added to the Classroom", student.Name);
                }
                _cache.Set("Class1", classRoom1);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Unable to add the Students");
            }
            return Ok(String.Format("Students: " + String.Join(", ", students.Select(s => s.Name)) + " added to the classroom"));

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
                roster = classRoom1.GetRoster();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            //changing the tuple to classroom object
            Classroom<Teacher, Student> classRoster = new Classroom<Teacher, Student>(roster.Item2, roster.Item1);

            //this will return the roster in Json format by default
            return Ok(classRoster);

        }

    }
}
