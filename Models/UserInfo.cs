using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public Int64 CategoryId { get; set; }
        public string Cellno { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public string Catidval { get; set; }
        public string Mfa { get; set; }
        public string Macaddress { get; set; }
        public string Ipaddress { get; set; }
        public string Recstatus { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Signature { get; set; }
        public byte[] Thumb { get; set; }
    }
}
