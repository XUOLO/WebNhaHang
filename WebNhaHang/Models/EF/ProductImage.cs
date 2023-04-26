﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models.EF
{
    [Table("Tb_ProductImage")]
    public class ProductImage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }

        public virtual Product Product { get; set; }
    }
}