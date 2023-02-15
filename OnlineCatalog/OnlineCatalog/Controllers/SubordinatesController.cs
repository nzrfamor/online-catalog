using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubordinatesController : ControllerBase
    {
        readonly ISubordinateService subordinateService;
        public SubordinatesController(ISubordinateService _subordinateService)
        {
            subordinateService = _subordinateService;
        }

        // GET: api/subordinates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubordinateModel>>> Get()
        {
            return new ObjectResult(await subordinateService.GetAllAsync());
        }

        // GET: api/subordinates/1
        [HttpGet("{id}")]
        public async Task<ActionResult<SubordinateModel>> Get(int id)
        {
            var subordinate = await subordinateService.GetByIdAsync(id);
            if (subordinate == null)
            {
                return NotFound();
            }
            return new ObjectResult(subordinate);
        }

        // POST: api/subordinates
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] SubordinateModel value)
        {
            try
            {
                await subordinateService.AddAsync(value);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(value);
        }

        // PUT: api/subordinates/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] SubordinateModel value)
        {
            try
            {
                value.Id = id;
                await subordinateService.UpdateAsync(value);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/subordinates/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await subordinateService.DeleteAsync(id);
            return Ok();
        }
    }
}