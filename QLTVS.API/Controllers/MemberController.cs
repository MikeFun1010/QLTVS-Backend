using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLTVS.BUS;
using QLTVS.DTO;

namespace QLTVS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberBUS _memberBUS;

        public MemberController(MemberBUS memberBUS) { _memberBUS = memberBUS; }

        [HttpPost("add-student")]
        public IActionResult AddStudent([FromBody] SinhVienDTO dto)
        {
            if (string.IsNullOrEmpty(dto.MaSv) || string.IsNullOrEmpty(dto.HoTen))
                return BadRequest("Thông tin không hợp lệ.");

            bool result = _memberBUS.CreateStudent(dto);

            if (result) return Ok(new { message = "Thêm sinh viên thành công." });
            return BadRequest(new { message = "Thêm thất bại (Mã SV có thể đã tồn tại)." });
        }

        [HttpPost("add-manager")]
        public IActionResult AddManager([FromBody] QuanLyDTO dto)
        {
            if (string.IsNullOrEmpty(dto.MaQl))
                return BadRequest("Mã quản lý không được để trống.");

            bool result = _memberBUS.CreateManager(dto);

            if (result) return Ok(new { message = "Thêm quản lý thành công." });
            return BadRequest(new { message = "Thêm thất bại." });
        }
    }
}
