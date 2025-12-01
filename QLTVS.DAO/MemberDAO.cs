using QLTVS.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace QLTVS.DAO
{
    public class MemberDAO // Giữ tên cũ của bạn
    {
        private readonly LibraryDbContext _context;
        public MemberDAO(LibraryDbContext context) { _context = context; }

        // Thêm Sinh Viên (Logic Transaction)
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

        // Thêm Quản Lý (Logic Transaction)
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
    }
}
