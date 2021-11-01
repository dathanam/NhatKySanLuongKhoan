using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace NKSLK_CS
{
    using NKSLK_CS.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;

    [Table("PhongBan")]
    public partial class PhongBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhongBan()
        {
            CongNhan = new HashSet<CongNhan>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string ten { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongNhan> CongNhan { get; set; }
    }

    class PhongBanList
    {

        // ThucCD Phong Ban 
        DBConecttion db;
        public PhongBanList()
        {
            db = new DBConecttion();
        }

        // Phuong Thuc Lay database 

        public List<PhongBan> getPhongBan(string id)
        {
            string sql;
            if (string.IsNullOrEmpty(id))
            {
                sql = "SELECT * FROM PhongBan";
            }
            else
            {
                sql = "SELECT * FROM PhongBan WHERE id = " + id;
            }
            List<PhongBan> listPhongBan = new List<PhongBan>();
            SqlConnection connection = db.GetConnection();
            SqlDataAdapter cmd = new SqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();

            // open connect :
            connection.Open();
            // do tat ca du lieu vao datatable
            cmd.Fill(dt);
            // dong ket noi
            cmd.Dispose();
            connection.Close();

            PhongBan phongBan;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                phongBan = new PhongBan();
                phongBan.id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                phongBan.ten = dt.Rows[i]["Ten"].ToString();

                listPhongBan.Add(phongBan);
            }
            return listPhongBan;
        }
        public void AddPhongBan(PhongBan phongBan)
        {
            string sql = "INSERT INTO PhongBan(ten) VALUES (N'" + phongBan.ten + "') ";
            SqlConnection connection = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
        }

        public void EditPhongBan(PhongBan phongBan)
        {
            string sql = "UPDATE PhongBan Set id= N'"+phongBan.id+"',ten=N'"+phongBan.ten+"' Where id = "+phongBan.id;
            SqlConnection connection = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
        }

        public void DeletePhongBan(PhongBan phongBan)
        {
            string sql = "DELETE From PhongBan Where id = "+phongBan.id;
            SqlConnection connection = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
        }
    }

}
