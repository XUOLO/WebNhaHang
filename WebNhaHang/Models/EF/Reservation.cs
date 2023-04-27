using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models.EF
{
    [Table("Tb_Reservation")]
    public class Reservation  :CommonAbstract
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        [StringLength(250)]
        public string Room { get; set; }
    
        public string NumberOfPeople { get; set; }
        public DateTime DateTime { get; set; }

    }
}