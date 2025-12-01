using QLTVS.DAO;
using QLTVS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.BUS
{
    public class AuthBUS
    {
        private readonly AuthDAO _authDAO;
        public AuthBUS(AuthDAO authDAO) { _authDAO = authDAO; }

        public LoginResponseDTO Authenticate(LoginRequestDTO request)
        {
            var account = _authDAO.GetAccountByCredentials(request);

            if (account == null)
            {
                return new LoginResponseDTO { IsSuccess = false, Message = "Tên đăng nhập hoặc mật khẩu không đúng." };
            }

            // Lấy ID (Ưu tiên MaSV, sau đó là MaQL)
            string userId = account.Masv ?? account.Maql;

            return new LoginResponseDTO
            {
                IsSuccess = true,
                Role = account.Vaitro,
                UserId = userId,
                Message = "Đăng nhập thành công!"
            };
        }
    }
}
