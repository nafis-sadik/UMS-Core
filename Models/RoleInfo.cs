using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RoleInfo
    {
        public long RoleId { get; set; }
        public string UserId { get; set; }
        public string Rolename { get; set; }
        public string Purpose { get; set; }
        public string Recstatus { get; set; }
    }
}
