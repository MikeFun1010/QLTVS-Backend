namespace QLTVS.DTO
{
    // DTO 1: LoginRequestDTO (Dữ liệu WinForms gửi lên API)
    public class LoginRequestDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }

    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Role { get; set; }    // "QuanLy" hoặc "SinhVien"
        public string UserId { get; set; }  // Mã SV hoặc Mã QL
        public string Message { get; set; }
    }
}


