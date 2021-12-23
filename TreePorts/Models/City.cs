using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class City
    {
        public City()
        {
            AdminUsers = new HashSet<AdminUser>();
            Agents = new HashSet<Agent>();
            SupportUsers = new HashSet<SupportUser>();
            Users = new HashSet<CaptainUser>();
        }

        public long Id { get; set; }
        public long? CountryId { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<AdminUser> AdminUsers { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<SupportUser> SupportUsers { get; set; }
        public virtual ICollection<CaptainUser> Users { get; set; }
    }
}
