using System;
using System.Collections.Generic;

namespace QLTVS.DAO.Models;

public partial class Tailieu
{
    public string Matailieu { get; set; } = null!;

    public string Tentailieu { get; set; } = null!;

    public string? Tacgia { get; set; }

    public int? Namxuatban { get; set; }

    public string? Nhaxuatban { get; set; }

    public string? Matheloai { get; set; }

    public int? Soluong { get; set; }

    public string? Linktailieu { get; set; }

    public virtual ICollection<Chitietmuonsach> Chitietmuonsaches { get; set; } = new List<Chitietmuonsach>();

    public virtual Theloai? MatheloaiNavigation { get; set; }
}
