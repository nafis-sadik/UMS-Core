using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Menu
    {
        public long MenuId { get; set; }
        public string FeatureId { get; set; }
        public string FeatureName { get; set; }
        public Nullable<long> ParentId { get; set; }
        public int FeatureType { get; set; }
        public string MenuName { get; set; }
        public int MenuSequence { get; set; }
        public string MenuLocation { get; set; }
        public string MenuStatus { get; set; }
    }
}
