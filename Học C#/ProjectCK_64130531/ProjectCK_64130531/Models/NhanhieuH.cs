using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProjectCK_64130531.Models
{
    public class NhanhieuH
    {
        public string tenNhanHieu { get; set; }
        public List<SanphamH> sanphamList { get; set; }
    }
}