using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models.EF
{
    [Table("Tb_Contact")]
    public class Contact:CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage ="ten khong duoc de trong")]
        [StringLength(150,ErrorMessage ="ten khong duoc qua 150 ki tu")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [StringLength(4000)]
        public string Message { get; set; }
        public int Isread { get; set; }
    }
}