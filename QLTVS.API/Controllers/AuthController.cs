using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthBUS _authBUS;

        public AuthController(AuthBUS authBUS) { _authBUS = authBUS; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.TenDangNhap) || string.IsNullOrEmpty(request.MatKhau))
                return BadRequest(new LoginResponseDTO { IsSuccess = false, Message = "Vui lòng nhập đầy đủ." });

            var response = _authBUS.Authenticate(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return Unauthorized(response);
        }
    }
}
