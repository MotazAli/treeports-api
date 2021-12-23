using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            UserAccountHistories = new HashSet<CaptainUserAccountHistory>();
            UserAccounts = new HashSet<CaptainUserAccount>();
            UserCurrentStatus = new HashSet<CaptainUserCurrentStatus>();
            UserStatusHistories = new HashSet<CaptainUserStatusHistory>();
        }

        public long Id { get; set; }
        public string Type { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CaptainUserAccountHistory> UserAccountHistories { get; set; }
        public virtual ICollection<CaptainUserAccount> UserAccounts { get; set; }
        public virtual ICollection<CaptainUserCurrentStatus> UserCurrentStatus { get; set; }
        public virtual ICollection<CaptainUserStatusHistory> UserStatusHistories { get; set; }
    }
}
