namespace TestCuoi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int eid { get; set; }
        [Required(ErrorMessage="Không bỏ trống!")]
        [DisplayName("Tên")]
        [StringLength(30)]
        public string name { get; set; }
        [Required(ErrorMessage = "Không bỏ trống!")]
        [DisplayName("Tuổi")]
        public int? age { get; set; }

        [Required(ErrorMessage = "Không bỏ trống!")]
        [DisplayName("Địa chỉ")]
        [StringLength(30)]

        public string addr { get; set; }
        [DisplayName("Lương")]
        public int? salary { get; set; }
        [Required(ErrorMessage = "Không bỏ trống!")]
        [DisplayName("ảnh")]
        [StringLength(50)]
        public string image { get; set; }

        public int? deptid { get; set; }

        public virtual Department Department { get; set; }
    }
}
