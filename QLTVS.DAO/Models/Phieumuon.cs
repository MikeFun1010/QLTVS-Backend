using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Phieumuon
{
    public string Maphieu { get; set; } = null!;

    public string Masv { get; set; } = null!;

    public DateOnly Ngaymuon { get; set; }

    public DateOnly Hantra { get; set; }

    public string? Trangthai { get; set; }

    public virtual ICollection<Chitietmuonsach> Chitietmuonsaches { get; set; } = new List<Chitietmuonsach>();

    public virtual Sinhvien MasvNavigation { get; set; } = null!;
}
