using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class HierarchyFilter
    {
        public HierarchyFilter()
        {
            //AdminUsers = new HashSet<AdminUser>();
            //Agents = new HashSet<Agent>();
            //SupportUsers = new HashSet<SupportUser>();
            //Supports = new HashSet<Support>();
            //Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Fullname { get; set; }
        public string CustomerName { get; set; }
        public long? StatusTypeId { get; set; }
        public long? ProductTypeId { get; set; }
        public long? CurrentStatus { get; set; }

        //public virtual ICollection<AdminUser> AdminUsers { get; set; }
        //public virtual ICollection<Agent> Agents { get; set; }
        //public virtual ICollection<SupportUser> SupportUsers { get; set; }
        //public virtual ICollection<Support> Supports { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}
