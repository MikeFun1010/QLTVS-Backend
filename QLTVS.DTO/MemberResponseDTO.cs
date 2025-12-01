using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTVS.DTO
{
    public class MemberResponseDTO
    {
        public string MaSv { get; set; }
        public string MaQl { get; set; }
        public string HoTen { get; set; }
        public string Lop { get; set; } // Nếu là QL thì null hoặc rỗng
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string Role { get; set; } // "SinhVien" hoặc "QuanLy"
    }
}
