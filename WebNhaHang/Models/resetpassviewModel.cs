using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models
{
    public class resetpassviewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
    }
}