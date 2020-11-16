using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DB
{
    public class UMS_AUTHQUE
    {
        [Key]
        public long AUTHQUEID { get; set; }
        public string ENTRYDATE { get; set; }
        public string FEATUREID { get; set; }
        public string APPID { get; set; }
        public string MODULEID { get; set; }
        public string TABLENAME { get; set; }
        public string PKVALUE { get; set; }
        public decimal MAXAUTHLEVEL { get; set; }
        public decimal APPAUTHLEVEL { get; set; }
        public string USERID { get; set; }
        public string ACTION { get; set; }
        public string REMARK { get; set; }
        public string NEWRECORD { get; set; }
        public string OLDRECORD { get; set; }
        public string OLDSTATUS { get; set; }
        public string RECSTATUS { get; set; }
        public string URLLINK { get; set; }
        public string ISAUTHCANCEL { get; set; }
    }
}
