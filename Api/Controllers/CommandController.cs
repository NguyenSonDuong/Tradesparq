using Api.Resp;
using Application.IRespostory.ICommand;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private ICommandRespostory _commandRespostory;

        public CommandController(ICommandRespostory commandRespostory)
        {
            _commandRespostory = commandRespostory;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("close")]
        public async Task Post(int id)
        {
            try
            {
                bool isSuccess = await _commandRespostory.CloseCommand(id);
                if (!isSuccess)
                {
                    throw new Exception("Close command failed");
                }
                Ok(new ResponsiveMessage()
                {
                    StatusCode = 200,
                    Message = "Close command success",
                    IsSuccess = true,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                Problem(detail: ex.Message, title: "Error Closing command");
            }
        }


        // POST api/<ValuesController>
        [HttpPost("create")]
        public async Task Create([FromQuery] string typeCommand, [FromQuery] string? comId, [FromQuery] string typeSearch, [FromQuery] string keysearch, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                bool isSuccess = await _commandRespostory.CreateCommand(typeCommand,comId,typeSearch, keysearch, startDate, endDate);
                if (!isSuccess)
                {
                    throw new Exception("Create command failed");
                }
                Ok(new ResponsiveMessage()
                {
                    StatusCode = 200,
                    Message = "Create command success",
                    IsSuccess = true,
                    Data = null
                });
            }
            catch(Exception ex)
            {
                Problem(detail: ex.Message, title: "Error creating command");
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpPost("delete")]
        public async Task Delete([FromQuery] int id)
        {
            try
            {
                await _commandRespostory.Delete(id);
                Ok(new ResponsiveMessage()
                {
                    StatusCode = 200,
                    Message = "Delete command success",
                    IsSuccess = true,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                Problem(detail: ex.Message, title: "Error deleting command");
            }
        }
    }
}
