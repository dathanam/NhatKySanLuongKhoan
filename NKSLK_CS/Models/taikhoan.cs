using NKSLK_CS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NKSLK_CS
{
    [Table("taikhoan")]
    public class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string tendangnhap { get; set; }

        [Required]
        [StringLength(50)]
        public string matkhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public int? id_cong_nhan { get; set; }

        public virtual CongNhan CongNhan { get; set; }

    }

    class DSTaiKhoan
    {
        /*1. Khởi tạo hàm contructor mặc định*/
        private NKSLK_Context context = null;

        DBConecttion db; 
        public DSTaiKhoan()
        {
            db = new DBConecttion();
            context = new NKSLK_Context();
        }
        /*phương thức lấy dữ liệu từ cơ sở dữ liệu*/
        public List<TaiKhoan> GetTaikhoans(string id)
        {
            string sql;
            if (string.IsNullOrEmpty(id)) /*nếu mà  không truyền id thì lấy toàn bộ dữ liệu từ CSDL */
            {
                sql = "Select * from taikhoan";
            }
            else
            {
                sql = "select * from taikhoan where id = " + id;
            }

            List<TaiKhoan> listtaikhoan = new List<TaiKhoan>(); /*tạo ra 1 danh sách tài khoản để lưu trữ các thông tin tài khoản*/
            SqlConnection con = db.GetConnection(); /*gọi hàm kết nối đến CSDL*/
            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);  /*thực thi câu lệnh*/

            /*sau khi lấy toàn bộ CSDL tiếp theo đổ CSDl lên bảng DataTable */
            DataTable dt = new DataTable();
            con.Open(); /*Mở kết nối*/
            cmd.Fill(dt);

            cmd.Dispose();/*ngắt câu lệnh đổ dữ liệu vào*/
            con.Close(); /*đóng kết nối*/

            /*sau khi đổ toàn bộ dữ liệu vào bảng đưa dữ liệu vừa đổ ra view */

            TaiKhoan taikhoan;   /*khai báo 1 đối tượng taikhoan*/
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                taikhoan = new TaiKhoan(); /*Tạo ra đối tương ở trong  vòng lặp */
                taikhoan.id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                taikhoan.tendangnhap = dt.Rows[i]["tendangnhap"].ToString();
                taikhoan.matkhau = dt.Rows[i]["matkhau"].ToString();
                taikhoan.id_cong_nhan = Convert.ToInt32(dt.Rows[i]["id_cong_nhan"].ToString());

                listtaikhoan.Add(taikhoan);
            }


            return listtaikhoan;

        }


        public void addTaiKhoan (TaiKhoan taiKhoan)
        {
            string sql = "insert into taikhoan (tendangnhap, matkhau, id_cong_nhan) values ('"+taiKhoan.tendangnhap+"','"+taiKhoan.matkhau+"','"+taiKhoan.id_cong_nhan+"')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql,con);

            con.Open();
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

        }

        public void editTaiKhoan (TaiKhoan taiKhoan)
        {
            string sql = "update taikhoan set tendangnhap ='"+taiKhoan.tendangnhap+"',matkhau= '"+taiKhoan.matkhau+"',id_cong_nhan= '"+taiKhoan.id_cong_nhan+"' where id =  "+taiKhoan.id;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);

            con.Open();
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

        }

        public void deleteTaiKhoan (TaiKhoan taiKhoan)
        {
            string sql = "Delete from taikhoan where id =" + taiKhoan.id;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);

            con.Open();
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();
        }
        
        public static DataSet ThucthiStore_DataSet(string StoredProcedure,
             params SqlParameter[] Parameters)
        {
            DBConecttion db = new DBConecttion();
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(StoredProcedure, con);
            if (Parameters != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(Parameters);
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(StoredProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            try
            {
                con.Open();
                da.Fill(ds);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
            }
            return ds;

        }



        public DataTable DangNhap(String tendangnhap, String matkhau)
        {
            SqlParameter[] arrParam =
            {
                new SqlParameter("@tendangnhap", SqlDbType.NVarChar),
                 new SqlParameter("@matkhau", SqlDbType.NVarChar)
            };
            arrParam[0].Value = tendangnhap;
            arrParam[1].Value = matkhau;
            return ThucthiStore_DataSet("DangNhap", arrParam).Tables[0];
        }


        public bool login (String tendangnhap , String matkhau)
        {
            Object[] sqlParams= 
            {
                new SqlParameter("@tendangnhap",tendangnhap),
                new SqlParameter ("@matkhau",matkhau),
            };
            var res = context.Database.SqlQuery<bool>("Sp_Account_login @tendangnhap,@matkhau", sqlParams).SingleOrDefault();
            return res;
        }

    }

}