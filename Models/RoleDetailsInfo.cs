using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RoleDetailsInfo
    {
        public string UserId { get; set; }
        public string RecStatus { get; set; }
        public string CancelYN { get; set; }
        public string AuthYN { get; set; }
        public string ViewDetailYN { get; set; }
        public string DeleteYN { get; set; }
        public string CreateYN { get; set; }
        public string FeatureId { get; set; }
        public string ModuleId { get; set; }
        public string AppId { get; set; }
        public long RoleId { get; set; }
        public long RoleDtlId { get; set; }
        public string EditYN { get; set; }
    }
}
