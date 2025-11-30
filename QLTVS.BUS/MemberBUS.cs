using QLTVS.DAO.Models;
using QLTVS.DAO;
using QLTVS.DTO;

namespace QLTVS.BUS
{
    public class MemberBUS
    {
        private readonly MemberDAO _memberDAO;

        // Dependency Injection nhận vào DAO
        public MemberBUS(MemberDAO memberDAO)
        {
            _memberDAO = memberDAO;
        }

        // Xử lý logic đăng nhập
        public Taikhoan? CheckLogin(LoginDTO dto)
        {
            return _memberDAO.GetAccount(dto.TenDangNhap, dto.MatKhau);
        }

        // Xử lý logic thêm sinh viên
        public bool CreateStudent(SinhVienDTO dto)
        {
            // 1. Map DTO -> Entity SinhVien
            Sinhvien sv = new Sinhvien
            {
                Masv = dto.MaSv,
                Hoten = dto.HoTen,
                Lop = dto.Lop,
                Email = dto.Email,
                Sdt = dto.Sdt
            };

            // 2. Tự tạo Entity TaiKhoan
            Taikhoan tk = new Taikhoan
            {
                Tendangnhap = dto.MaSv,   // Lấy Mã SV làm Username
                Matkhau = "123456",       // Mật khẩu mặc định
                Vaitro = "SinhVien",      // Set cứng vai trò
                Masv = dto.MaSv           // Link khóa ngoại
            };

            return _memberDAO.InsertStudent(sv, tk);
        }

        // Xử lý logic thêm quản lý
        public bool CreateManager(QuanLyDTO dto)
        {
            Quanly ql = new Quanly
            {
                Maql = dto.MaQl,
                Hoten = dto.HoTen,
                Email = dto.Email,
                Sdt = dto.Sdt
            };

            Taikhoan tk = new Taikhoan
            {
                Tendangnhap = dto.MaQl,
                Matkhau = "admin123",     // Mật khẩu mặc định cho QL
                Vaitro = "QuanLy",
                Maql = dto.MaQl
            };

            return _memberDAO.InsertManager(ql, tk);
        }
    }
}
