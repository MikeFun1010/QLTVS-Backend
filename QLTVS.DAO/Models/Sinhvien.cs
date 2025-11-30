using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Sinhvien
{
    public string Masv { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string? Lop { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<Phieumuon> Phieumuons { get; set; } = new List<Phieumuon>();

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
