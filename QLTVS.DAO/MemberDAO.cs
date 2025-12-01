using QLTVS.DAO.Models;
using Microsoft.EntityFrameworkCore;
using QLTVS.DTO;

namespace QLTVS.DAO
{
    public class MemberDAO
    {
        private readonly LibraryDbContext _context;
        public MemberDAO(LibraryDbContext context) { _context = context; }

        // [GET] Lấy tất cả thành viên (Đã sửa để lấy HoTen, Email...)
        public List<MemberResponseDTO> GetAllMembers()
        {
            var query = from tk in _context.Taikhoans
                            // Join trái với SinhVien
                        join sv in _context.Sinhviens on tk.Masv equals sv.Masv into svGroup
                        from subSv in svGroup.DefaultIfEmpty()
                            // Join trái với QuanLy
                        join ql in _context.Quanlies on tk.Maql equals ql.Maql into qlGroup
                        from subQl in qlGroup.DefaultIfEmpty()
                        select new MemberResponseDTO
                        {
                            MaSv = tk.Masv,
                            MaQl = tk.Maql,
                            // Lấy tên từ bảng SV, nếu không có thì lấy từ bảng QL
                            HoTen = subSv != null ? subSv.Hoten : (subQl != null ? subQl.Hoten : "N/A"),
                            Lop = subSv != null ? subSv.Lop : "",
                            Email = subSv != null ? subSv.Email : (subQl != null ? subQl.Email : ""),
                            Sdt = subSv != null ? subSv.Sdt : (subQl != null ? subQl.Sdt : ""),
                            Role = tk.Vaitro
                        };

            return query.ToList();
        }

        // [POST] Thêm Sinh Viên (Code cũ OK)
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

        // [POST] Thêm Quản Lý (Code cũ OK)
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

        // [DELETE] Xóa thành viên (Code cũ OK)
        public bool DeleteMember(string ma, string loai)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var account = loai == "SV"
                    ? _context.Taikhoans.FirstOrDefault(t => t.Masv == ma)
                    : _context.Taikhoans.FirstOrDefault(t => t.Maql == ma);

                if (account != null) _context.Taikhoans.Remove(account);

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
