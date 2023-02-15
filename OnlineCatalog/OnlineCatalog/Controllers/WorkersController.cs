using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        readonly IWorkerService workerService;
        public WorkersController(IWorkerService _workerService)
        {
            workerService = _workerService;
        }

        // GET: api/workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerModel>>> Get()
        {
            return new ObjectResult(await workerService.GetAllAsync());
        }

        // GET: api/workers/leader/1
        [HttpGet("leader/{id}")]
        public async Task<ActionResult<IEnumerable<WorkerModel>>> GetWorkersByLeaderId(int id)
        {
            return new ObjectResult(await workerService.GetWorkersByLeaderIdAsync(id));
        }

        // GET: api/workers/subordinate/1
        [HttpGet("subordinate/{id}")]
        public async Task<ActionResult<WorkerModel>> GetWorkerBySubordinateId(int id)
        {
            var worker = await workerService.GetWorkerBySubordinateIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return new ObjectResult(worker);
        }



        // GET: api/workers/leaders//subordinate/1
        [HttpGet("leaders/subordinate/{id}")]
        public async Task<ActionResult<IEnumerable<WorkerModel>>> GetWorkersAsPossibleLeadersBySubordinateId(int id)
        {
            var workers = await workerService.GetWorkerAsPossibleLeadersBySubordinateIdAsync(id);
            if (workers == null)
            {
                return NotFound();
            }
            return new ObjectResult(workers);
        }

        // GET: api/workers/1
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerModel>> Get(int id)
        {
            var worker = await workerService.GetByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return new ObjectResult(worker);
        }

        // POST: api/workers
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] WorkerModel value)
        {
            try
            {
                await workerService.AddAsync(value);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(value);
        }

        // PUT: api/workers/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] WorkerModel value)
        {
            try
            {
                value.Id = id;
                await workerService.UpdateAsync(value);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/workers/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await workerService.DeleteAsync(id);
            return Ok();
        }
    }
}
