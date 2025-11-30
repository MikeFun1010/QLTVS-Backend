using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Quanly
{
    public string Maql { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
