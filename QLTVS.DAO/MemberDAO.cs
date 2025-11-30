using QLTVS.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLTVS.DAO
{
    public class MemberDAO
    {
        private readonly LibraryDbContext _context;

        public MemberDAO(LibraryDbContext context)
        {
            _context = context;
        }

        // 1. Kiểm tra đăng nhập
        public Taikhoan? GetAccount(string username, string password)
        {
            // Tìm tài khoản khớp username và password
            return _context.Taikhoans
                .FirstOrDefault(x => x.Tendangnhap == username && x.Matkhau == password);
        }

        // 2. Thêm Sinh Viên (Gồm thông tin SV + Tài khoản)
        public bool InsertStudent(Sinhvien sv, Taikhoan tk)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Bước 1: Thêm thông tin sinh viên
                _context.Sinhviens.Add(sv);
                _context.SaveChanges();

                // Bước 2: Thêm tài khoản (Lúc này MaSV đã tồn tại hợp lệ)
                _context.Taikhoans.Add(tk);
                _context.SaveChanges();

                transaction.Commit(); // Xác nhận thành công cả 2
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Nếu lỗi thì hoàn tác
                // Console.WriteLine(ex.Message); // Ghi log lỗi để debug nếu cần
                return false;
            }
        }

        // 3. Thêm Quản Lý (Gồm thông tin QL + Tài khoản)
        public bool InsertManager(Quanly ql, Taikhoan tk)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Quanlies.Add(ql);
                _context.SaveChanges();

                _context.Taikhoans.Add(tk);
                _context.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
