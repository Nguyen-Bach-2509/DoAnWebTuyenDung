using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace DoAnWebTuyenDung.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DoAnWebEntities")
        { }

    }
}