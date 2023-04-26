using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models
{
    public abstract class CommonAbstract
    {
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifieDate { get; set; }
        public string ModifieBy { get; set; }

    }
}