using Api.Resp;
using Application.IRespostory.IAuthen;
using Domain.Entities.Authen;
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
        public async Task<ActionResult> Get()
        {
            try
            {
                List<AuthenTradesparq> list = await _authenTradesparqRespostory.GetAll();
                return Ok(new ResponsiveMessage()
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Lấy danh sách token thành công",
                    Data = list
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
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

        [HttpPost("token/deactive")]
        public async Task<ActionResult> DeactiveTokenAsync([FromQuery] int id)
        {
            try
            {
                bool isSuccess = await _authenTradesparqRespostory.Deactive(id);
                if (!isSuccess)
                {
                    return BadRequest(
                        new ResponsiveMessage()
                        {
                            StatusCode = 200,
                            IsSuccess = true,
                            Message = "Hủy token không thành công! Vui lòng kiểm tra dữ liệu đầu vào",
                            Data = null
                        }
                    );
                }
                return Ok(new ResponsiveMessage()
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Hủy token thành công",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
