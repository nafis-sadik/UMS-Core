using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DB
{
    public class UMS_APPCONFIG
    {
        private UMS_APPCONFIG() { }
        [Key]
        string PKVALUE { get; set; }
        int MAXAUTHLEVEL { get; set; }
        int APPAUTHLEVEL { get; set; }
        string USERID { get; set; }
        string ACTION { get; set; }
        string REMARK { get; set; }
        string NEWRECORD { get; set; }
        string OLDRECORD { get; set; }
        string OLDSTATUS { get; set; }
        string RECSTATUS { get; set; }
        string URLLINK { get; set; }
        string ISAUTHCANCEL { get; set; }
    }
}
