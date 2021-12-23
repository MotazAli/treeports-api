using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class BonusType
    {
        public BonusType()
        {
            UserBonus = new HashSet<CaptainUserBonus>();
        }

        public long Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<CaptainUserBonus> UserBonus { get; set; }
    }
}
