using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderController : ControllerBase
    {
        readonly ILeaderService leaderService;
        public LeaderController(ILeaderService _leaderService)
        {
            this.leaderService = _leaderService;
        }

        // GET: api/leaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderModel>>> Get()
        {
            return new ObjectResult(await leaderService.GetAllAsync());
        }

        // GET: api/leaders/1
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaderModel>> Get(int id)
        {
            var leader = await leaderService.GetByIdAsync(id);
            if (leader == null)
            {
                return NotFound();
            }
            return new ObjectResult(leader);
        }

        // GET: api/leaders/worker/1
        [HttpGet("worker/{id}")]
        public async Task<ActionResult<LeaderModel>> GetByWorkerId(int id)
        {
            var leader = await leaderService.GetByWorkerIdAsync(id);
            if (leader == null)
            {
                return NotFound();
            }
            return new ObjectResult(leader);
        }

        // POST: api/leaders
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] LeaderModel value)
        {
            try
            {
                await leaderService.AddAsync(value);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(value);
        }

        // PUT: api/leaders/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LeaderModel value)
        {
            try
            {
                value.Id = id;
                await leaderService.UpdateAsync(value);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/leaders/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await leaderService.DeleteAsync(id);
            return Ok();
        }
    }
}
