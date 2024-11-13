using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWebTuyenDung.Models
{
    public class JobViewModel
    {
        public IEnumerable<DoAnWebTuyenDung.Models.Job> Jobs { get; set; }
        public IEnumerable<DoAnWebTuyenDung.Models.Job_Categories> JobCategories { get; set; }
    }
}
