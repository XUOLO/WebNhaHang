using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebNhaHang.Models.EF;
namespace WebNhaHang.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không được để  trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để  trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để  trống")]
        public string Address { get; set; }
        public string Mail { get; set; }
        public int TypePayment { get; set; } 
        public User user { get; set; }
 
        public List<ShoppingCartItem> items { get; set; }

    }
     

}