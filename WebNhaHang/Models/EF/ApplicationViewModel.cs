using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models.EF
{
    public class ApplicationViewModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ApplicantID { get; set; }
        [StringLength(250)]
        public string FullName { get; set; }
        [StringLength(250)]
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [StringLength(500)]
        [Required]
        public string Address { get; set; }
        public string Position { get; set; }
        public string Experience { get; set; }
        public IEnumerable<Aplication> Aplications { get; set; }
    }
}