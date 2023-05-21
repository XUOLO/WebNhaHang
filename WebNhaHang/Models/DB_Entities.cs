using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("DefaultConnection") { }
        
    }
}