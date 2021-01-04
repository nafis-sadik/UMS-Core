using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RoleDetailAuth
    {
        public string FeatureId { get; set; }
        public string Create { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
        public string Details { get; set; }
        public string Auth { get; set; }
        public int AuthLevel { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Menu> Menu { get; set; }
    }
}
