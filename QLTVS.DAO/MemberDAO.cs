using QLTVS.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLTVS.DAO
{
    public class MemberDAO
    {
        private readonly LibraryDbContext _context;
        public MemberDAO(LibraryDbContext context) { _context = context; }

        // [GET] Lấy tất cả tài khoản
        public List<Taikhoan> GetAllAccounts()
        {
            // Trả về tất cả Taikhoan (cần mapping/join ở tầng trên để lấy HoTen, Lop)
            return _context.Taikhoans.ToList();
        }

        // [POST] Thêm Sinh Viên (Tạo TK)
        public bool InsertStudent(Sinhvien sv, Taikhoan tk)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Sinhviens.Add(sv); _context.SaveChanges();
                _context.Taikhoans.Add(tk); _context.SaveChanges();
                transaction.Commit(); return true;
            }
            catch { transaction.Rollback(); return false; }
        }

        // [POST] Thêm Quản Lý (Tạo TK)
        public bool InsertManager(Quanly ql, Taikhoan tk)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Quanlies.Add(ql); _context.SaveChanges();
                _context.Taikhoans.Add(tk); _context.SaveChanges();
                transaction.Commit(); return true;
            }
            catch { transaction.Rollback(); return false; }
        }

        // [DELETE] Xóa thành viên (Xóa 2 bảng)
        public bool DeleteMember(string ma, string loai)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Xóa Tài khoản
                var account = loai == "SV"
                    ? _context.Taikhoans.FirstOrDefault(t => t.Masv == ma)
                    : _context.Taikhoans.FirstOrDefault(t => t.Maql == ma);
                if (account != null) _context.Taikhoans.Remove(account);

                // Xóa thông tin cá nhân
                if (loai == "SV")
                {
                    var sv = _context.Sinhviens.FirstOrDefault(s => s.Masv == ma);
                    if (sv != null) _context.Sinhviens.Remove(sv);
                }
                else
                {
                    var ql = _context.Quanlies.FirstOrDefault(q => q.Maql == ma);
                    if (ql != null) _context.Quanlies.Remove(ql);
                }

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch { transaction.Rollback(); return false; }
        }
    }
}
