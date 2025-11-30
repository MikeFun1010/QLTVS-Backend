using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Taikhoan
{
    public string Tendangnhap { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public string? Vaitro { get; set; }

    public string? Masv { get; set; }

    public string? Maql { get; set; }

    public virtual Quanly? MaqlNavigation { get; set; }

    public virtual Sinhvien? MasvNavigation { get; set; }
}
