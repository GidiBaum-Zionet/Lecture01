using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementLib;
using ElementLib.Enties;
using ElementLib.Interfaces;
using ElementLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ElementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElementController : ControllerBase
    {
        readonly ILogger<ElementController> _Logger;
        readonly IElementRepository _ElementRepository;
        readonly ElementService _ElementService;

        public ElementController(
            ILogger<ElementController> logger,
            IElementRepository elementRepository,
            ElementService elementService)
        {
            _Logger = logger;
            _ElementRepository = elementRepository;
            _ElementService = elementService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    _Logger.LogInformation("OK");
        //    return Ok("OK");
        //}

        [HttpGet("Elements")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Element>>> GetElements()
        {
            var list = await _ElementRepository.ReadAsync();

            return Ok(list.Select(e => e.ToModel()));
        }

        [HttpGet("Elements/Table")]
        [Authorize(Roles = "Admin")]
        public async Task<FileContentResult> GetElementTable()
        {
            var list = await _ElementRepository.ReadAsync();

            var json = JsonConvert.SerializeObject(list);
            byte[] bytes = Encoding.ASCII.GetBytes(json);

            return File(bytes, "application/json", "ElementTable.json");
        }

        [HttpPost("Elements/Table")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostElementTable(IFormFile file)
        {
            try
            {
                await using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                var elementList = JsonConvert.DeserializeObject<List<ElementEntity>>(json);

                await _ElementRepository.InsertAsync(elementList);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Parse/{formula}")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<Molecule>> Parse(string formula)
        {
            try
            {
                var molecule = await _ElementService.ParseAsync(formula);
                _Logger.LogInformation($"Parse: {molecule}");

                var parts = molecule.Parts
                    .Select(r => new { element = r.Element, number = r.Number });
                return Ok(parts);
            }
            catch
            {
                _Logger.LogError($"Parse error on: {formula}");
                return BadRequest();
            }
        }

        [HttpGet("Weight/{formula}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<double>> CalcWeight(string formula)
        {
            try
            {
                var mol = await _ElementService.ParseAsync(formula);

                _Logger.LogInformation("OK");
                return Ok(mol.CalcMass());
            }
            catch
            {
                _Logger.LogError($"Parse error on: {formula}");
                return BadRequest();
            }
        }

        [HttpDelete("Elements")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteElements()
        {
            await _ElementRepository.DeleteAllAsync();

            return Ok();
        }
    }
}
