using Api.Resp;
using Application.IRespostory.IAuthen;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/v1/tradesparq")]
    [ApiController]
    public class AuthenTradesparqController : ControllerBase
    {
        IAuthenTradesparqRespostory _authenTradesparqRespostory;

        public AuthenTradesparqController(IAuthenTradesparqRespostory authenTradesparqRespostory)
        {
            _authenTradesparqRespostory = authenTradesparqRespostory;
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

        // POST api/<ValuesController>
        [HttpPost("token")]
        public async Task<ActionResult> SaveTokenAsync([FromQuery] string token, [FromQuery] string dataSource)
        {
            try
            {
                bool isSuccess = await _authenTradesparqRespostory.Create(token, dataSource);
                if (!isSuccess)
                {
                    return BadRequest(
                        new ResponsiveMessage()
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Lưu token không thành công! Vui lòng kiểm tra dữ liệu đầu vào",
                            Data = null
                        }
                    );
                }
                return Ok(new ResponsiveMessage()
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lưu token thành công",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
