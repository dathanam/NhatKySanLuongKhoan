using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using NKSLK_CS.Models;

namespace NKSLK_CS
{
    public class ThongKe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public string ten { set; get; }
        public string gioi_tinh { set; get; }
        public string chuc_vu { set; get; }
        public string que_quan { set; get; }

        public string luong_hop_dong { set; get; }
        public string luong_bao_hiem { set; get; }

        public string tenphongban { set; get; }

        public string thoigianden { set; get; }

        public string thoigianve { set; get; }

        public int id { set; get; }

        public string ngay_sinh { set; get; }

        public string TongSoCong { set; get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual CongNhan CongNhan { get; set; }

        public virtual DanhMucCongNhanThucHienKhoan DanhMucCongNhanThucHienKhoan{get; set;}

        public virtual SanLuongKhoanTheoCa SanLuongKhoanTheoCa { set; get; }

        public virtual CaLamViec CaLamViec { set; get; }

        
    }
    
    class DSThongKe
    {
        DBConecttion db;
        public DSThongKe()
        {
            db = new DBConecttion();
        }
        /*phương thức lấy dữ liệu từ cơ sở dữ liệu*/
        public List<ThongKe> DSThongKes(string id)
        {
            string sql;
            if (string.IsNullOrEmpty(id)) /*nếu mà  không truyền id thì lấy toàn bộ dữ liệu từ CSDL */
            {
                // sql = "select CongNhan.ten, CongNhan.gioi_tinh,CongNhan.chuc_vu, CongNhan.que_quan, CongNhan.luong_hop_dong,CongNhan.luong_bao_hiem,PhongBan.ten as 'tenphongban',danhmuccongnhanthuchienkhoan.thoi_gian_den,danhmuccongnhanthuchienkhoan.thoi_gian_ve   from CongNhan join PhongBan on CongNhan.id_phong_ban = PhongBan.id join danhmuccongnhanthuchienkhoan on danhmuccongnhanthuchienkhoan.id_cong_nhan= congnhan.id ";
                //sql = "select CongNhan.ten , CongNhan.gioi_tinh, CongNhan.chuc_vu, CongNhan.que_quan, CongNhan.luong_hop_dong, CongNhan.luong_bao_hiem  from CongNhan join DanhMucCongNhanThucHienKhoan on CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhanjoin SanLuongKhoanTheoCa on DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id join CaLamViec on CaLamViec.id = SanLuongKhoanTheoCa.id_ca";

                sql = "  With cau13(id, tenCN, SoCongTrongNgay) as(select CongNhan.id, CongNhan.ten, CASE SanLuongKhoanTheoCa.id_ca WHEN 1 THEN Convert(float, (DATEDIFF(hour, DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve))) / 8 WHEN 2 THEN Convert(float, (DATEDIFF(hour, DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve))) / 8 WHEN 3 THEN 1.3 * convert(float, ((24 + (DATEDIFF(hour, DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)))) / 8) END as SoCong From CongNhan, DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan Where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and month(NhatKySanLuongKhoan.ngay) = '8') select cau13.id, cau13.tenCN, CongNhan.ngay_sinh, SUM(cau13.SoCongTrongNgay) as TongSoCong from cau13, CongNhan where CongNhan.id = cau13.id group by cau13.id, cau13.tenCN, CongNhan.ngay_sinh  ";
            }
            else
            {
                sql = "select * from congnhan where id = " + id;
            }

            List<ThongKe> listthongke = new List<ThongKe>(); /*tạo ra 1 danh sách tài khoản để lưu trữ các thông tin tài khoản*/
            SqlConnection con = db.GetConnection(); /*gọi hàm kết nối đến CSDL*/
                SqlDataAdapter cmd = new SqlDataAdapter(sql, con);  /*thực thi câu lệnh*/

            /*sau khi lấy toàn bộ CSDL tiếp theo đổ CSDl lên bảng DataTable */
            DataTable dt = new DataTable();
            con.Open(); /*Mở kết nối*/
            cmd.Fill(dt);

            cmd.Dispose();/*ngắt câu lệnh đổ dữ liệu vào*/
            con.Close(); /*đóng kết nối*/

            /*sau khi đổ toàn bộ dữ liệu vào bảng đưa dữ liệu vừa đổ ra view */

            ThongKe thongke;   /*khai báo 1 đối tượng taikhoan*/
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                thongke = new ThongKe(); /*Tạo ra đối tương ở trong  vòng lặp */
                thongke.id = Convert.ToInt32( dt.Rows[i]["id"].ToString());
                thongke.ten = dt.Rows[i]["tenCN"].ToString();
                thongke.ngay_sinh = dt.Rows[i]["ngay_sinh"].ToString();
                thongke.TongSoCong = dt.Rows[i]["TongSoCong"].ToString();
                

                listthongke.Add(thongke);
            }


            return listthongke;

        }
    }
}