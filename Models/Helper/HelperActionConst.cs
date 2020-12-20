using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMSApi.Helpers
{
    public static class HelperActionConst
    {
        public const string Insert = "I", 
                            Update = "U", 
                            Cancel = "C", 
                            Decline = "D", 
                            Pending = "P",
                            Authorized = "A",

                            SessionActive="A",
                            SessionInactive="I",
                            SessionForcedStop="F",

                            Allowed = "Y",
                            NotAllowed = "N";

    }

    public static class HelperSequenceConst
    {
        public static string Role = "UMS_ROLE_ROLEID_SEQ";
        public static string RoleDetail = "UMS_ROLEDTL_ROLEDTLID_SEQ";
        public static string RoleAssign = "UMS_ROLEASSIGN_ROLEASSIGNID_SE";
        public static string AuthQueue = "UMS_AUTHQUE_AUTHQUEID_SEQ";
        public static string NotiFicationAssign = "UMS_NOTIFICATION_ASSIGN_SEQ";
        public static string NotiFication = "UMS_NOTIFICATION_SEQ";
        
    }
    public static class UserLevelConst
    {
        public const int BranchLevel = 1, WingLevel = 2, SectionLevel = 3;
    }
    public static class NotificationTypeConst
    {
        public const int
                           WrongCredential = 1,
                           PasswordMismatch = 2,
                           LoginFromAnotherDevice = 3,
                           NewInvestigation = 11,
                           Approval = 12,
                           Distributation = 13,
                           Return = 14,
                           Store = 15,
                           Dispatch = 16,
                           CustomNotification = 18,
                           Modification = 17;
    }
}