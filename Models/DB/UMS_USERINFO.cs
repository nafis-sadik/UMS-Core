using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DB
{
    public class UMS_USERINFO
    {
        [Key]
        public string USERID { get; set; }
        public string NAME { get; set; }    
        public decimal CATEGORYID { get; set; }
        public string CELLNO { get; set; }
        public string EMAIL { get; set; }
        public DateTime DOB { get; set; }
        public string CATIDVAL { get; set; }
        public string MFA { get; set; }
        public string MACADDRESS { get; set; }
        public string IPADDRESS { get; set; }
        public string RECSTATUS { get; set; }
        public string PICTURE { get; set; }
        public string SIGNATURE { get; set; }
        public string THUMB { get; set; }
    }
}
