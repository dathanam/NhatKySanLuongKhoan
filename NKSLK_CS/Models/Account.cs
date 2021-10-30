namespace NKSLK_CS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string username { get; set; }

        [Required]
        [StringLength(200)]
        public string mat_khau { get; set; }

        public int quyen { get; set; }
    }
}
