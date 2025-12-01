namespace QLTVS.DTO
{
    // DTO 1: LoginRequestDTO (Dữ liệu WinForms gửi lên API)
    public class LoginRequestDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }

    // DTO 2: LoginResponseDTO (Dữ liệu API trả về cho WinForms)
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Role { get; set; }    // Vai trò: "QuanLy" hoặc "SinhVien"
        public string UserId { get; set; }  // Mã ID (MaSV hoặc MaQL)
        public string Message { get; set; } // Thông báo kết quả
    }
}
