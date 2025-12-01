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

        // [GET] Lấy tất cả
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var members = _memberBUS.GetAllMembers();
            // *LƯU Ý: WinForms sẽ cần map list Taikhoan này thành MemberResponseDTO*
            return Ok(members);
        }

        // [POST] Thêm Sinh Viên
        [HttpPost("add-student")]
        public IActionResult AddStudent([FromBody] SinhVienDTO dto)
        {
            if (string.IsNullOrEmpty(dto.MaSv) || string.IsNullOrEmpty(dto.HoTen))
                return BadRequest("Thông tin không hợp lệ.");

            bool result = _memberBUS.CreateStudent(dto);

            if (result) return Ok(new { message = "Thêm sinh viên thành công." });
            return BadRequest(new { message = "Thêm thất bại (Mã SV có thể đã tồn tại)." });
        }

        // [POST] Thêm Quản Lý
        [HttpPost("add-manager")]
        public IActionResult AddManager([FromBody] QuanLyDTO dto)
        {
            if (string.IsNullOrEmpty(dto.MaQl))
                return BadRequest("Mã quản lý không được để trống.");

            bool result = _memberBUS.CreateManager(dto);

            if (result) return Ok(new { message = "Thêm quản lý thành công." });
            return BadRequest(new { message = "Thêm thất bại." });
        }

        // [DELETE] Xóa thành viên
        [HttpDelete("delete/{loai}/{ma}")]
        public IActionResult DeleteMember(string loai, string ma)
        {
            if (string.IsNullOrEmpty(ma) || (loai != "SV" && loai != "QL"))
            {
                return BadRequest(new { message = "Tham số không hợp lệ." });
            }

            bool result = _memberBUS.DeleteMember(ma, loai);

            if (result) return Ok(new { message = "Xóa thành viên thành công." });
            return NotFound(new { message = "Không tìm thấy thành viên để xóa." });
        }
    }
}
