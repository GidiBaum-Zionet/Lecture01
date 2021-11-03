using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        readonly IWeatherGenerator _Generator;

        public DemoController(ILogger<DemoController> logger, IWeatherGenerator generator)
        {
            _logger = logger;
            _Generator = generator;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _Generator.GetData();
        }

        [HttpGet("student")]
        public Student GetStudent()
        {
            var s = new Student();

            s.Name = "Mickey";
            s.Id = 117;

            return s;
        }

        [HttpPost("student")]
        public ActionResult<Student> PostStudent(string name, int id)
        {
            if (id == 0)
            {
                return BadRequest("Id is zero");
            }

            var s = new Student();

            s.Name = name;
            s.Id = id;

            return Ok(s);
        }

    }
}