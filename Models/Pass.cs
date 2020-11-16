using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class Pass
    {
        public long Passid { get; set; }
        public string Userid { get; set; }
        public string Userpass { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime Expiredate { get; set; }
        public string Recstatus { get; set; }
    }
}
