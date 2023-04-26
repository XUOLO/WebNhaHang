using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models.EF
{
    [Table("Tb_Category")]
    public class Category: CommonAbstract
    {
      
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage ="Tên danh mục không được để trống")]
        [StringLength(150)]
        public string Title {get;set;}
        public string Alias { get; set; }
        public string Description{get;set;}
        [StringLength(150)]
        public string SeoTitle {get;set;}
        [StringLength(250)]
        public string SeoDescription{get;set;}
        [StringLength(150)]
        public string SeoKeywords{get;set;}
        public int Position{get;set;}
        public bool IsActive { get; set; }

    
       
    }
}