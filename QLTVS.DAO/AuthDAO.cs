using QLTVS.DAO.Models;
using QLTVS.DTO;
using Microsoft.EntityFrameworkCore;


namespace QLTVS.DAO
{
    public class AuthDAO
    {
        private readonly LibraryDbContext _context;
        public AuthDAO(LibraryDbContext context) { _context = context; }

        public Taikhoan? GetAccountByCredentials(LoginRequestDTO request)
        {
            return _context.Taikhoans
                .FirstOrDefault(tk =>
                    tk.Tendangnhap == request.TenDangNhap &&
                    tk.Matkhau == request.MatKhau);
        }
    }
}
