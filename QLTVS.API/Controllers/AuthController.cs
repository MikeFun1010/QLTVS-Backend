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
        private readonly MemberBUS _memberBUS;

        public AuthController(MemberBUS memberBUS)
        {
            _memberBUS = memberBUS;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var user = _memberBUS.CheckLogin(loginDto);

            if (user == null)
            {
                return Unauthorized("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Trả về JSON chứa thông tin user + Vai trò
            return Ok(new
            {
                Username = user.Tendangnhap,
                Role = user.Vaitro, // Quan trọng: Winform sẽ dùng cái này để phân quyền
                MaSV = user.Masv,
                MaQL = user.Maql
            });
        }
    }
}
