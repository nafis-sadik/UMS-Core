using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AuthQueDataModel
    {
        public string ActionType { get; set; }
        public string FeatureId { get; set; }
        public string TableName { get; set; }
        public string PKId { get; set; }
        public string UserId { get; set; }
        public string UrlLink { get; set; }
        public string NewRecord { get; set; }
        public string OldRecord { get; set; }
        public string OldStatus { get; set; }
    }
}
