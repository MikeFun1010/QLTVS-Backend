using QLTVS.DAO.Models;
using QLTVS.DAO;
using QLTVS.DTO;

namespace QLTVS.BUS
{
    public class MemberBUS
    {
        private readonly MemberDAO _memberDAO;
        public MemberBUS(MemberDAO memberDAO) { _memberDAO = memberDAO; }

        // Thêm Sinh Viên (Tạo TK tự động)
        public bool CreateStudent(SinhVienDTO dto)
        {
            Sinhvien sv = new Sinhvien { Masv = dto.MaSv, Hoten = dto.HoTen, Lop = dto.Lop, Email = dto.Email, Sdt = dto.Sdt };
            Taikhoan tk = new Taikhoan { Tendangnhap = dto.MaSv, Matkhau = "123456", Vaitro = "SinhVien", Masv = dto.MaSv };

            return _memberDAO.InsertStudent(sv, tk);
        }

        // Thêm Quản Lý (Tạo TK tự động)
        public bool CreateManager(QuanLyDTO dto)
        {
            Quanly ql = new Quanly { Maql = dto.MaQl, Hoten = dto.HoTen, Email = dto.Email, Sdt = dto.Sdt };
            Taikhoan tk = new Taikhoan { Tendangnhap = dto.MaQl, Matkhau = "admin123", Vaitro = "QuanLy", Maql = dto.MaQl };

            return _memberDAO.InsertManager(ql, tk);
        }
    }
}
