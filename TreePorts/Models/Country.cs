using System;
using System.Collections.Generic;

namespace TreePorts.Models
{
    public partial class Country
    {
        public Country()
        {
            CaptainUsers = new HashSet<CaptainUser>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Iso { get; set; }
        public long? Code { get; set; }
        public string? ArabicName { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencyArabicName { get; set; }
        public string? CurrencyIso { get; set; }
        public string? CreatedBy { get; set; }
        public long? CreatedByType { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedByType { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<CaptainUser> CaptainUsers { get; set; }
    }
}
