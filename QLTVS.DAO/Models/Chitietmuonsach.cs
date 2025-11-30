using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Chitietmuonsach
{
    public string Maphieu { get; set; } = null!;

    public string Matailieu { get; set; } = null!;

    public int? Soluong { get; set; }

    public string? Tinhtrangmuon { get; set; }

    public string? Tinhtrangtra { get; set; }

    public DateOnly? Ngaytratailieu { get; set; }

    public virtual Phieumuon MaphieuNavigation { get; set; } = null!;

    public virtual Tailieu MatailieuNavigation { get; set; } = null!;
}
