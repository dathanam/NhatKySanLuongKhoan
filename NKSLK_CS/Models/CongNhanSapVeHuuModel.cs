
namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CongNhanSapVeHuuModel
    {
        public int id { get; set; }

        public string ten { get; set; }

        public DateTime? ngay_sinh { get; set; }

        public string gioi_tinh { get; set; }

        public string chuc_vu { get; set; }

        public string que_quan { get; set; }

        public double luong_hop_dong { get; set; }

        public double luong_bao_hiem { get; set; }

        public int? id_phong_ban { get; set; }

        public int? id_phuong { get; set; }
    }
}