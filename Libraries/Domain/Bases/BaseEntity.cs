using System;

namespace Onion.Libraries.Domain.Bases
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime LastModifiedOnUtc { get; set; }

        public int LastModifierId { get; set; }

        public bool ISUpdated { get; set; }
    }   
}