using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            AdminCurrentStatus = new HashSet<AdminCurrentStatus>();
            AdminUserAccounts = new HashSet<AdminUserAccount>();
            AgentCurrentStatus = new HashSet<AgentCurrentStatus>();
            Agents = new HashSet<Agent>();
            CaptainUserAccountHistories = new HashSet<CaptainUserAccountHistory>();
            CaptainUserAccounts = new HashSet<CaptainUserAccount>();
            CaptainUserActivities = new HashSet<CaptainUserActivity>();
            CaptainUserCurrentStatus = new HashSet<CaptainUserCurrentStatus>();
            CaptainUserStatusHistories = new HashSet<CaptainUserStatusHistory>();
            SupportUserAccounts = new HashSet<SupportUserAccount>();
            SupportUserCurrentStatus = new HashSet<SupportUserCurrentStatus>();
            SupportUserWorkingStates = new HashSet<SupportUserWorkingState>();
        }

        public long Id { get; set; }
        public string? Type { get; set; }
        public string? ArabicType { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<AdminCurrentStatus> AdminCurrentStatus { get; set; }
        public virtual ICollection<AdminUserAccount> AdminUserAccounts { get; set; }
        public virtual ICollection<AgentCurrentStatus> AgentCurrentStatus { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<CaptainUserAccountHistory> CaptainUserAccountHistories { get; set; }
        public virtual ICollection<CaptainUserAccount> CaptainUserAccounts { get; set; }
        public virtual ICollection<CaptainUserActivity> CaptainUserActivities { get; set; }
        public virtual ICollection<CaptainUserCurrentStatus> CaptainUserCurrentStatus { get; set; }
        public virtual ICollection<CaptainUserStatusHistory> CaptainUserStatusHistories { get; set; }
        public virtual ICollection<SupportUserAccount> SupportUserAccounts { get; set; }
        public virtual ICollection<SupportUserCurrentStatus> SupportUserCurrentStatus { get; set; }
        public virtual ICollection<SupportUserWorkingState> SupportUserWorkingStates { get; set; }
    }
}
