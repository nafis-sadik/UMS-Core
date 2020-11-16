using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DB
{
    public class UMS_PASS
    {
        public long PASSID { get; set; }
        public string USERID { get; set; }
        public string USERPASS { get; set; }
        public DateTime CREATEDATE { get; set; }
        public DateTime EXPIREDATE { get; set; }
        public string RECSTATUS { get; set; }
    }
}
